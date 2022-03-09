using TOS.CQRS.Executions.Commands;

namespace TOS.CQRS.Handlers.Commands
{
    public interface IAsyncCommandHandler<in TAsyncCommand> : IAsyncExecutionHandler<TAsyncCommand>
        where TAsyncCommand : IAsyncCommand
    {        
    }

    public interface IAsyncCommandHandler<in TAsyncCommand, TResult> : IAsyncExecutionHandler<TAsyncCommand, TResult>
        where TAsyncCommand : IAsyncCommand<TResult>
    {    
    }
}
