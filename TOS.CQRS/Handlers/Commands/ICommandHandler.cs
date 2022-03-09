using TOS.CQRS.Executions.Commands;

namespace TOS.CQRS.Handlers.Commands
{
    public interface ICommandHandler<in TCommand> : IExecutionHandler<TCommand>
        where TCommand : ICommand
    {
    }

    public interface ICommandHandler<in TCommand, out TResult> : IExecutionHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
    }
}
