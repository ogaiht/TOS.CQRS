using System.Threading.Tasks;
using TOS.CQRS.Executions;

namespace TOS.CQRS.Handlers
{
    public interface IRequestExecutor
    {
        void Execute<TExecutionRequest, TExecutionHandler>(TExecutionRequest executionRequest, TExecutionHandler executionHandler)
            where TExecutionRequest : IExecutionRequest
            where TExecutionHandler : IExecutionHandler<TExecutionRequest>;
        TResult Execute<TExecutionRequest, TExecutionHandler, TResult>(TExecutionRequest executionRequest, TExecutionHandler executionHandler)
            where TExecutionRequest : IExecutionRequest<TResult>
            where TExecutionHandler : IExecutionHandler<TExecutionRequest, TResult>;
        Task ExecuteAsync<TAsyncExecutionRequest, TAsyncExecutionHandler>(TAsyncExecutionRequest asyncExecutionRequest, TAsyncExecutionHandler asyncExecutionHandler)
            where TAsyncExecutionRequest : IAsyncExecutionRequest
            where TAsyncExecutionHandler : IAsyncExecutionHandler<TAsyncExecutionRequest>;
        Task<TResult> ExecuteAsync<TAsyncExecutionRequest, TAsyncExecutionHandler, TResult>(TAsyncExecutionRequest asyncExecutionRequest, TAsyncExecutionHandler asyncExecutionHandler)
            where TAsyncExecutionRequest : IAsyncExecutionRequest<TResult>
            where TAsyncExecutionHandler : IAsyncExecutionHandler<TAsyncExecutionRequest, TResult>;
    }
}