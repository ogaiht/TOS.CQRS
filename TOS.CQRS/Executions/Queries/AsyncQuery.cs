namespace TOS.CQRS.Executions.Queries
{
    public abstract class AsyncQuery<TResult> : ExecutionRequest<TResult>, IAsyncQuery<TResult>
    {

    }
}
