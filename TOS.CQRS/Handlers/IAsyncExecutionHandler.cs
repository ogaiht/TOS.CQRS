using System.Threading.Tasks;
using TOS.CQRS.Executions;

namespace TOS.CQRS.Handlers
{
    public interface IAsyncExecutionHandler<in TAsyncExecution> where TAsyncExecution : IAsyncExecutionRequest
    {
        Task ExecuteAsync(TAsyncExecution execution);
    }

    public interface IAsyncExecutionHandler<in TAsyncExecution, TResult> where TAsyncExecution : IAsyncExecutionRequest<TResult>
    {
        Task<TResult> ExecuteAsync(TAsyncExecution execution);
    }
}
