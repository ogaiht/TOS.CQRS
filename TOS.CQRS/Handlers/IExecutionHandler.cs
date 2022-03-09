using TOS.CQRS.Executions;

namespace TOS.CQRS.Handlers
{
    public interface IExecutionHandler<in TExecution> where TExecution : IExecutionRequest
    {
        void Execute(TExecution execution);
    }

    public interface IExecutionHandler<in TExecution, out TResult> where TExecution : IExecutionRequest<TResult>
    {
        TResult Execute(TExecution execution);
    }
}
