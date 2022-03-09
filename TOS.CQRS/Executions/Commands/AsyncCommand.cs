using System;

namespace TOS.CQRS.Executions.Commands
{
    public abstract class AsyncCommand : IAsyncCommand
    {
        protected AsyncCommand()
            : this(Guid.NewGuid())
        {
        }

        protected AsyncCommand(Guid executionId)
        {
            ExecutionId = executionId;
        }

        public Guid ExecutionId { get; }
    }

    public abstract class AsyncCommand<TResult> : AsyncCommand, IAsyncCommand<TResult>
    {
        protected AsyncCommand()
        {
        }

        protected AsyncCommand(Guid executionId)
            : base(executionId)
        {
        }
    }
}
