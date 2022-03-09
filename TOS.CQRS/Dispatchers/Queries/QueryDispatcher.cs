using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TOS.CQRS.Executions.Queries;
using TOS.CQRS.Handlers;
using TOS.CQRS.Handlers.Queries;

namespace TOS.CQRS.Dispatchers.Queries
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IRequestExecutor _handlerExecutor;
        private readonly IExecutionHandlerProvider _executionHandlerProvider;
        private readonly ILogger<QueryDispatcher> _logger;

        public QueryDispatcher(IRequestExecutor handlerExecutor,
            IExecutionHandlerProvider executionHandlerProvider,
            ILogger<QueryDispatcher> logger)
        {
            _handlerExecutor = handlerExecutor;
            _executionHandlerProvider = executionHandlerProvider;
            _logger = logger;
        }

        public TResult Execute<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            IQueryHandler<TQuery, TResult> handler = _executionHandlerProvider.GetHandler<TQuery, IQueryHandler<TQuery, TResult>, TResult>();
            try
            {
                return _handlerExecutor.Execute<TQuery, IQueryHandler<TQuery, TResult>, TResult>(query, handler);
            }
            catch (Exception ex)
            {
                LogError<TQuery>(ex, handler);
                throw;
            }
        }

        public async Task<TResult> ExecuteAsync<TAsyncQuery, TResult>(TAsyncQuery query) where TAsyncQuery : IAsyncQuery<TResult>
        {
            IAsyncQueryHandler<TAsyncQuery, TResult> handler = _executionHandlerProvider.GetAsyncHandler<TAsyncQuery, IAsyncQueryHandler<TAsyncQuery, TResult>, TResult>();
            try
            {
                return await _handlerExecutor.ExecuteAsync<TAsyncQuery, IAsyncQueryHandler<TAsyncQuery, TResult>, TResult>(query, handler);
            }
            catch (Exception ex)
            {
                LogError<TAsyncQuery>(ex, handler);
                throw;
            }
        }

        private void LogError<T>(Exception ex, object handler)
        {
            _logger.LogError(ex, "Error when execution {Query} by handler {Handler}.", typeof(T).FullName, handler.GetType().FullName);
        }
    }

}
