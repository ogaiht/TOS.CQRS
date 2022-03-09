using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TOS.Common.Utils;
using TOS.CQRS.Executions;
using TOS.CQRS.Handlers;

namespace TOS.CQRS.Dispatchers
{
    public class ExecutionHandlerProvider : IExecutionHandlerProvider
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IExceptionHelper _exceptionHelper;
        private readonly ILogger<ExecutionHandlerProvider> _logger;

        public ExecutionHandlerProvider(
            IServiceProvider serviceProvider,
            IExceptionHelper exceptionHelper,
            ILogger<ExecutionHandlerProvider> logger)
        {
            _serviceProvider = serviceProvider;
            _exceptionHelper = exceptionHelper;
            _logger = logger;
        }

        public TAsyncHandler GetAsyncHandler<TAsyncRequest, TAsyncHandler>(bool throwExceptionIfNotFound = true)
            where TAsyncRequest : IAsyncExecutionRequest
            where TAsyncHandler : IAsyncExecutionHandler<TAsyncRequest>
        {
            return GetHandler<TAsyncHandler>(throwExceptionIfNotFound);
        }

        public TAsyncHandler GetAsyncHandler<TAsyncRequest, TAsyncHandler, TResult>(bool throwExceptionIfNotFound = true)
            where TAsyncRequest : IAsyncExecutionRequest<TResult>
            where TAsyncHandler : IAsyncExecutionHandler<TAsyncRequest, TResult>
        {
            return GetHandler<TAsyncHandler>(throwExceptionIfNotFound);
        }

        public THandler GetHandler<TRequest, THandler>(bool throwExceptionIfNotFound = true)
            where TRequest : IExecutionRequest
            where THandler : IExecutionHandler<TRequest>
        {
            return GetHandler<THandler>(throwExceptionIfNotFound);
        }

        public THandler GetHandler<TRequest, THandler, TResult>(bool throwExceptionIfNotFound = true)
            where TRequest : IExecutionRequest<TResult>
            where THandler : IExecutionHandler<TRequest, TResult>
        {
            return GetHandler<THandler>(throwExceptionIfNotFound);
        }

        public IEnumerable<THandler> GetHandlers<TRequest, THandler>(bool throwExceptionIfNotFound = true)
            where TRequest : IExecutionRequest
            where THandler : IExecutionHandler<TRequest>
        {
            return GetHandlers<THandler>(throwExceptionIfNotFound);
        }

        public IEnumerable<THandler> GetHandlers<TRequest, THandler, TResult>(bool throwExceptionIfNotFound = true)
            where TRequest : IExecutionRequest<TResult>
            where THandler : IExecutionHandler<TRequest, TResult>
        {
            return GetHandlers<THandler>(throwExceptionIfNotFound);
        }

        public IEnumerable<TAsyncHandler> GetAsyncHandlers<TAsyncRequest, TAsyncHandler>(bool throwExceptionIfNotFound = true)
            where TAsyncRequest : IAsyncExecutionRequest
            where TAsyncHandler : IAsyncExecutionHandler<TAsyncRequest>
        {
            return GetHandlers<TAsyncHandler>(throwExceptionIfNotFound);
        }

        public IEnumerable<TAsyncHandler> GetAsyncHandlers<TAsyncRequest, TAsyncHandler, TResult>(bool throwExceptionIfNotFound = true)
            where TAsyncRequest : IAsyncExecutionRequest<TResult>
            where TAsyncHandler : IAsyncExecutionHandler<TAsyncRequest, TResult>
        {
            return GetHandlers<TAsyncHandler>(throwExceptionIfNotFound);
        }

        private T GetHandler<T>(bool throwExceptionIfNotFound)
        {
            _logger.LogInformation("Getting handler for '{0}'.", typeof(T).FullName);
            T handler = _serviceProvider.GetService<T>();
            _exceptionHelper.CheckInvalidOperationException(handler == null && throwExceptionIfNotFound, "No handler was found for " + typeof(T).FullName);
            return handler;
        }

        private IEnumerable<T> GetHandlers<T>(bool throwExceptionIfNotFound)
        {
            _logger.LogInformation("Getting handler for '{0}'.", typeof(T).FullName);
            IEnumerable<T> handlers = _serviceProvider.GetServices<T>();
            _exceptionHelper.CheckInvalidOperationException((handlers == null || !handlers.Any()) && throwExceptionIfNotFound, "No handler was found for " + typeof(T).FullName);
            return handlers;
        }
    }
}
