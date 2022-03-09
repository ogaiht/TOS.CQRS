using TOS.CQRS.Executions;
using TOS.CQRS.Handlers;

namespace TOS.CQRS.Logging
{
    public class HandlerExecutorIdGenerator : IHandlerExecutorIdGenerator
    {
        public string CreateExecutionId<TExecution, THandler>(TExecution execution, THandler handler)
            where TExecution : IExecutionRequest
            where THandler : IExecutionHandler<TExecution>
        {
            return CreateId(execution, handler);
        }

        public string CreateExecutionId<TExecution, THandler, TResult>(TExecution execution, THandler handler)
            where TExecution : IExecutionRequest<TResult>
            where THandler : IExecutionHandler<TExecution, TResult>
        {
            return CreateId(execution, handler);
        }

        public string CreateExecutionIdForAsync<TExecution, THandler>(TExecution execution, THandler handler)
            where TExecution : IAsyncExecutionRequest
            where THandler : IAsyncExecutionHandler<TExecution>
        {
            return CreateId(execution, handler);
        }

        public string CreateExecutionIdForAsync<TExecution, THandler, TResult>(TExecution execution, THandler handler)
            where TExecution : IAsyncExecutionRequest<TResult>
            where THandler : IAsyncExecutionHandler<TExecution, TResult>
        {
            return CreateId(execution, handler);
        }

        private static string CreateId<TExecution>(TExecution execution, object handler) where TExecution : IExecutionRequest
        {
            return $"{handler.GetType().FullName}:{execution.GetType().FullName}:{execution.ExecutionId}";
        }
    }
}
