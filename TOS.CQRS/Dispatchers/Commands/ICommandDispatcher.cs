using System.Threading.Tasks;
using TOS.CQRS.Executions.Commands;

namespace TOS.CQRS.Dispatchers.Commands
{
    public interface ICommandDispatcher
    {
        void Execute<TCommand>(TCommand command) where TCommand : ICommand;
        TResult Execute<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>;
        Task ExecuteAsync<TAsyncCommand>(TAsyncCommand asyncCommand) where TAsyncCommand : IAsyncCommand;
        Task<TResult> ExecuteAsync<TAsyncCommand, TResult>(TAsyncCommand asyncCommand) where TAsyncCommand : IAsyncCommand<TResult>;
    }
}
