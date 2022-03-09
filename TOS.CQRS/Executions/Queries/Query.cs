namespace TOS.CQRS.Executions.Queries
{
    public abstract class Query<TResult> : ExecutionRequest<TResult>, IQuery<TResult>
    {
        
    }
}
