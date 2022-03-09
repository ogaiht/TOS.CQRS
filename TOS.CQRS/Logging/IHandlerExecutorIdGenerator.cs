using TOS.CQRS.Executions;
using TOS.CQRS.Handlers;

namespace TOS.CQRS.Logging
{
    public interface IHandlerExecutorIdGenerator
    {
        string CreateExecutionId<TExecution, THandler>(TExecution execution, THandler handler)
            where TExecution : IExecutionRequest
            where THandler : IExecutionHandler<TExecution>;
        string CreateExecutionId<TExecution, THandler, TResult>(TExecution execution, THandler handler)
            where TExecution : IExecutionRequest<TResult>
            where THandler : IExecutionHandler<TExecution, TResult>;
        string CreateExecutionIdForAsync<TExecution, THandler>(TExecution execution, THandler handler)
            where TExecution : IAsyncExecutionRequest
            where THandler : IAsyncExecutionHandler<TExecution>;
        string CreateExecutionIdForAsync<TExecution, THandler, TResult>(TExecution execution, THandler handler)
            where TExecution : IAsyncExecutionRequest<TResult>
            where THandler : IAsyncExecutionHandler<TExecution, TResult>;
    }
}
