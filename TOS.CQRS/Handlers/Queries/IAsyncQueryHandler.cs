using TOS.CQRS.Executions.Queries;

namespace TOS.CQRS.Handlers.Queries
{
    public interface IAsyncQueryHandler<in TQuery, TResult> : IAsyncExecutionHandler<TQuery, TResult> where TQuery : IAsyncQuery<TResult>
    {
        
    }
}
