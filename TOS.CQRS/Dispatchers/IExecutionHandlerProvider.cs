using System.Collections.Generic;
using TOS.CQRS.Executions;
using TOS.CQRS.Handlers;

namespace TOS.CQRS.Dispatchers
{
    public interface IExecutionHandlerProvider
    {
        //T GetHandler<T>(bool throwExceptionIfNotFound = true);
        //IEnumerable<T> GetHandlers<T>(bool throwExceptionIfNotFound = true);

        THandler GetHandler<TRequest, THandler>(bool throwExceptionIfNotFound = true)
            where TRequest : IExecutionRequest
            where THandler : IExecutionHandler<TRequest>;
        THandler GetHandler<TRequest, THandler, TResult>(bool throwExceptionIfNotFound = true)
            where TRequest : IExecutionRequest<TResult>
            where THandler : IExecutionHandler<TRequest, TResult>;
        TAsyncHandler GetAsyncHandler<TAsyncRequest, TAsyncHandler>(bool throwExceptionIfNotFound = true)
            where TAsyncRequest : IAsyncExecutionRequest
            where TAsyncHandler : IAsyncExecutionHandler<TAsyncRequest>;
        TAsyncHandler GetAsyncHandler<TAsyncRequest, TAsyncHandler, TResult>(bool throwExceptionIfNotFound = true)
            where TAsyncRequest : IAsyncExecutionRequest<TResult>
            where TAsyncHandler : IAsyncExecutionHandler<TAsyncRequest, TResult>;
        IEnumerable<THandler> GetHandlers<TRequest, THandler>(bool throwExceptionIfNotFound = true)
            where TRequest : IExecutionRequest
            where THandler : IExecutionHandler<TRequest>;
        IEnumerable<THandler> GetHandlers<TRequest, THandler, TResult>(bool throwExceptionIfNotFound = true)
            where TRequest : IExecutionRequest<TResult>
            where THandler : IExecutionHandler<TRequest, TResult>;
        IEnumerable<TAsyncHandler> GetAsyncHandlers<TAsyncRequest, TAsyncHandler>(bool throwExceptionIfNotFound = true)
            where TAsyncRequest : IAsyncExecutionRequest
            where TAsyncHandler : IAsyncExecutionHandler<TAsyncRequest>;
        IEnumerable<TAsyncHandler> GetAsyncHandlers<TAsyncRequest, TAsyncHandler, TResult>(bool throwExceptionIfNotFound = true)
            where TAsyncRequest : IAsyncExecutionRequest<TResult>
            where TAsyncHandler : IAsyncExecutionHandler<TAsyncRequest, TResult>;
    }
}