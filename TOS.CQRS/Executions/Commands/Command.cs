using System;

namespace TOS.CQRS.Executions.Commands
{
    public abstract class Command : ICommand
    {
        protected Command()
            : this(Guid.NewGuid())
        {
        }

        protected Command(Guid executionId)
        {
            ExecutionId = executionId;
        }

        public Guid ExecutionId { get; }
    }

    public abstract class Command<TResult> : Command, ICommand<TResult>
    {
        protected Command()
        {
        }

        protected Command(Guid executionId)
            : base(executionId)
        {
        }
    }
}
