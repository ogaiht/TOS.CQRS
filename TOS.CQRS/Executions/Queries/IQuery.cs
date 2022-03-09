namespace TOS.CQRS.Executions.Queries
{
    public interface IQuery<out TResult> : IExecutionRequest<TResult>
    {
    }
}
