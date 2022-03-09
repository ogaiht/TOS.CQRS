using Microsoft.Extensions.Logging;
using System;
using TOS.Common.Serialization.Json;
using TOS.CQRS.Executions;
using TOS.CQRS.Handlers;

namespace TOS.CQRS.Logging
{
    public class HandlerExecutorLogger : IHandlerExecutorLogger
    {
        private readonly IJsonSerializer _jsonSerializer;
        private readonly ILogger<RequestExecutor> _logger;
        private readonly IHandlerExecutorIdGenerator _handlerExecutorIdGenerator;

        public HandlerExecutorLogger(
            IJsonSerializer jsonSerializer,
            ILogger<RequestExecutor> logger,
            IHandlerExecutorIdGenerator handlerExecutorIdGenerator
            )
        {
            _jsonSerializer = jsonSerializer;
            _logger = logger;
            _handlerExecutorIdGenerator = handlerExecutorIdGenerator;
        }

        private Lazy<string> GetSerializedRequest(object request)
        {
            return new Lazy<string>(() => _jsonSerializer.Serialize(request));
        }

        public IHandlerExecutorLoggerScope CreateScope<TExecution, THandler>(TExecution execution, THandler handler)
            where TExecution : IExecutionRequest
            where THandler : IExecutionHandler<TExecution>
        {
            string executionId = _handlerExecutorIdGenerator.CreateExecutionId(execution, handler);
            return new HandlerExecutorLoggerScope(_logger, executionId, GetSerializedRequest(executionId));
        }

        public IHandlerExecutorLoggerScope<TResult> CreateScope<TExecution, THandler, TResult>(TExecution execution, THandler handler)
            where TExecution : IExecutionRequest<TResult>
            where THandler : IExecutionHandler<TExecution, TResult>
        {
            string executionId = _handlerExecutorIdGenerator.CreateExecutionId<TExecution, THandler, TResult>(execution, handler);
            return new HandlerExecutorLoggerScope<TResult>(_logger, executionId, GetSerializedRequest(execution), _jsonSerializer);
        }

        public IHandlerExecutorLoggerScope CreateScopeForAsync<TExecution, THandler>(TExecution execution, THandler handler)
            where TExecution : IAsyncExecutionRequest
            where THandler : IAsyncExecutionHandler<TExecution>
        {
            string executionId = _handlerExecutorIdGenerator.CreateExecutionIdForAsync(execution, handler);
            return new HandlerExecutorLoggerScope(_logger, executionId, GetSerializedRequest(executionId));
        }

        public IHandlerExecutorLoggerScope<TResult> CreateScopeForAsync<TExecution, THandler, TResult>(TExecution execution, THandler handler)
            where TExecution : IAsyncExecutionRequest<TResult>
            where THandler : IAsyncExecutionHandler<TExecution, TResult>
        {
            string executionId = _handlerExecutorIdGenerator.CreateExecutionIdForAsync<TExecution, THandler, TResult>(execution, handler);
            return new HandlerExecutorLoggerScope<TResult>(_logger, executionId, GetSerializedRequest(execution), _jsonSerializer);
        }
    }
}
