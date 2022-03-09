using TOS.CQRS.Executions;
using TOS.CQRS.Handlers;

namespace TOS.CQRS.Logging
{
    public interface IHandlerExecutorLogger
    {
        IHandlerExecutorLoggerScope CreateScope<TExecution, THandler>(TExecution execution, THandler handler)
            where TExecution : IExecutionRequest
            where THandler : IExecutionHandler<TExecution>;
        IHandlerExecutorLoggerScope<TResult> CreateScope<TExecution, THandler, TResult>(TExecution execution, THandler handler)
            where TExecution : IExecutionRequest<TResult>
            where THandler : IExecutionHandler<TExecution, TResult>;
        IHandlerExecutorLoggerScope CreateScopeForAsync<TExecution, THandler>(TExecution execution, THandler handler)
            where TExecution : IAsyncExecutionRequest
            where THandler : IAsyncExecutionHandler<TExecution>;
        IHandlerExecutorLoggerScope<TResult> CreateScopeForAsync<TExecution, THandler, TResult>(TExecution execution, THandler handler)
            where TExecution : IAsyncExecutionRequest<TResult>
            where THandler : IAsyncExecutionHandler<TExecution, TResult>;
    }
}
