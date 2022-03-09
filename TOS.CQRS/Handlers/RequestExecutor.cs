using System;
using System.Threading.Tasks;
using TOS.CQRS.Executions;
using TOS.CQRS.Logging;

namespace TOS.CQRS.Handlers
{
    public class RequestExecutor : IRequestExecutor
    {
        private readonly IHandlerExecutorLogger _handlerExecutorLogger;

        public RequestExecutor(IHandlerExecutorLogger handlerExecutorLogger)
        {
            _handlerExecutorLogger = handlerExecutorLogger;
        }

        public void Execute<TExecutionRequest, TExecutionHandler>(TExecutionRequest executionRequest, TExecutionHandler executionHandler)
            where TExecutionRequest : IExecutionRequest
            where TExecutionHandler : IExecutionHandler<TExecutionRequest>
        {
            using (IHandlerExecutorLoggerScope logger = _handlerExecutorLogger.CreateScope(executionRequest, executionHandler))
            {
                logger.BeforeExecution();
                try
                {
                    using (logger.TimeExecution())
                    {
                        executionHandler.Execute(executionRequest);
                    }
                    logger.AfterExecution();
                }
                catch (Exception ex)
                {
                    logger.OnError(ex);
                    throw;
                }
            }
        }

        public TResult Execute<TExecutionRequest, TExecutionHandler, TResult>(TExecutionRequest executionRequest, TExecutionHandler executionHandler)
            where TExecutionRequest : IExecutionRequest<TResult>
            where TExecutionHandler : IExecutionHandler<TExecutionRequest, TResult>
        {
            using (IHandlerExecutorLoggerScope<TResult> logger = _handlerExecutorLogger.CreateScope<TExecutionRequest, TExecutionHandler, TResult>(executionRequest, executionHandler))
            {
                logger.BeforeExecution();
                try
                {
                    TResult result;
                    using (logger.TimeExecution())
                    {
                        result = executionHandler.Execute(executionRequest);
                    }
                    logger.AfterExecution(result);
                    return result;
                }
                catch (Exception ex)
                {
                    logger.OnError(ex);
                    throw;
                }
            }
        }

        public async Task ExecuteAsync<TAsyncExecutionRequest, TAsyncExecutionHandler>(TAsyncExecutionRequest asyncExecutionRequest, TAsyncExecutionHandler asyncExecutionHandler)
            where TAsyncExecutionRequest : IAsyncExecutionRequest
            where TAsyncExecutionHandler : IAsyncExecutionHandler<TAsyncExecutionRequest>
        {
            using (IHandlerExecutorLoggerScope logger = _handlerExecutorLogger.CreateScopeForAsync(asyncExecutionRequest, asyncExecutionHandler))
            {
                logger.BeforeExecution();
                try
                {
                    using (logger.TimeExecution())
                    {
                        await asyncExecutionHandler.ExecuteAsync(asyncExecutionRequest);
                    }
                    logger.AfterExecution();
                }
                catch (Exception ex)
                {
                    logger.OnError(ex);
                    throw;
                }
            }
        }

        public async Task<TResult> ExecuteAsync<TAsyncExecutionRequest, TAsyncExecutionHandler, TResult>(TAsyncExecutionRequest asyncExecutionRequest, TAsyncExecutionHandler asyncExecutionHandler)
            where TAsyncExecutionRequest : IAsyncExecutionRequest<TResult>
            where TAsyncExecutionHandler : IAsyncExecutionHandler<TAsyncExecutionRequest, TResult>
        {
            using (IHandlerExecutorLoggerScope<TResult> logger = _handlerExecutorLogger.CreateScopeForAsync<TAsyncExecutionRequest, TAsyncExecutionHandler, TResult>(asyncExecutionRequest, asyncExecutionHandler))
            {
                logger.BeforeExecution();
                try
                {
                    TResult result;
                    using (logger.TimeExecution())
                    {
                        result = await asyncExecutionHandler.ExecuteAsync(asyncExecutionRequest);
                    }
                    logger.AfterExecution(result);
                    return result;
                }
                catch (Exception ex)
                {
                    logger.OnError(ex);
                    throw;
                }
            }
        }
    }
}
