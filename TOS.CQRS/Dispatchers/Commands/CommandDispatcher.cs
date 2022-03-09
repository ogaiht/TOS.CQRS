using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TOS.CQRS.Executions.Commands;
using TOS.CQRS.Handlers;
using TOS.CQRS.Handlers.Commands;

namespace TOS.CQRS.Dispatchers.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IRequestExecutor _handlerExecutor;
        private readonly IExecutionHandlerProvider _executionHandlerProvider;
        private readonly ILogger<CommandDispatcher> _logger;

        public CommandDispatcher(
            IRequestExecutor handlerExecutor,
            IExecutionHandlerProvider executionHandlerProvider,
            ILogger<CommandDispatcher> logger)
        {
            _handlerExecutor = handlerExecutor;
            _executionHandlerProvider = executionHandlerProvider;
            _logger = logger;
        }

        public void Execute<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            ICommandHandler<TCommand> handler = _executionHandlerProvider.GetHandler<TCommand, ICommandHandler<TCommand>>();
            try
            {
                _handlerExecutor.Execute(command, handler);
            }
            catch (Exception ex)
            {
                LogError<TCommand>(ex, handler);
                throw;
            }
        }

        public TResult Execute<TCommand, TResult>(TCommand command)
            where TCommand : ICommand<TResult>
        {
            ICommandHandler<TCommand, TResult> handler = _executionHandlerProvider.GetHandler<TCommand, ICommandHandler<TCommand, TResult>, TResult>();
            try
            {
                return _handlerExecutor.Execute<TCommand, ICommandHandler<TCommand, TResult>, TResult>(command, handler);
            }
            catch (Exception ex)
            {
                LogError<TCommand>(ex, handler);
                throw;
            }
        }

        public async Task ExecuteAsync<TAsyncCommand>(TAsyncCommand asyncCommand)
            where TAsyncCommand : IAsyncCommand
        {
            IAsyncCommandHandler<TAsyncCommand> asyncHandler = _executionHandlerProvider.GetAsyncHandler<TAsyncCommand, IAsyncCommandHandler<TAsyncCommand>>();
            try
            {
                await _handlerExecutor.ExecuteAsync(asyncCommand, asyncHandler);
            }
            catch (Exception ex)
            {
                LogError<TAsyncCommand>(ex, asyncHandler);
                throw;
            }
        }

        public async Task<TResult> ExecuteAsync<TAsyncCommand, TResult>(TAsyncCommand asyncCommand)
            where TAsyncCommand : IAsyncCommand<TResult>
        {
            IAsyncCommandHandler<TAsyncCommand, TResult> asyncHandler = _executionHandlerProvider.GetAsyncHandler<TAsyncCommand, IAsyncCommandHandler<TAsyncCommand, TResult>, TResult>();
            try
            {
                return await _handlerExecutor.ExecuteAsync<TAsyncCommand, IAsyncCommandHandler<TAsyncCommand, TResult>, TResult>(asyncCommand, asyncHandler);
            }
            catch (Exception ex)
            {
                LogError<TAsyncCommand>(ex, asyncHandler);
                throw;
            }
        }

        private void LogError<T>(Exception ex, object handler)
        {
            _logger.LogError(ex, "Error when executing {Command} by handler {Handler}.", typeof(T).FullName, handler.GetType().FullName);
        }
    }
}
