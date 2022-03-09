using System;

namespace TOS.CQRS.Executions
{
    public abstract class ExecutionRequest : IExecutionRequest
    {
        protected ExecutionRequest()
            : this(Guid.NewGuid())
        {

        }

        protected ExecutionRequest(Guid executionId)
        {
            ExecutionId = executionId;
        }

        public Guid ExecutionId { get; }
    }

    public abstract class ExecutionRequest<TResult> : ExecutionRequest, IExecutionRequest<TResult>
    {
        protected ExecutionRequest()
        {

        }

        protected ExecutionRequest(Guid executionId)
            : base(executionId)
        {
        }
    }
}
