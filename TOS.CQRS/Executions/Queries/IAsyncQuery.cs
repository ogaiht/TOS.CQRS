namespace TOS.CQRS.Executions.Queries
{
    public interface IAsyncQuery<out TResult> : IAsyncExecutionRequest<TResult>
    {
    }
}
