using System.Threading.Tasks;
using TOS.CQRS.Executions.Queries;

namespace TOS.CQRS.Dispatchers.Queries
{
    public interface IQueryDispatcher
    {
        TResult Execute<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
        Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query) where TQuery : IAsyncQuery<TResult>;
    }
}