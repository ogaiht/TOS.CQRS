using TOS.CQRS.Executions.Queries;

namespace TOS.CQRS.Handlers.Queries
{
    public interface IQueryHandler<in TQuery, out TResult> : IExecutionHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {        
    }
}
