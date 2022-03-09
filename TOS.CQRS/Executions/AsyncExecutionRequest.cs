using System;

namespace TOS.CQRS.Executions
{
    public abstract class AsyncExecutionRequest : IAsyncExecutionRequest
    {
        protected AsyncExecutionRequest()
            : this(Guid.NewGuid())
        {

        }

        protected AsyncExecutionRequest(Guid executionId)
        {
            ExecutionId = executionId;
        }

        public Guid ExecutionId { get; }
    }

    public abstract class AsyncExecutionRequest<TResult> : AsyncExecutionRequest, IAsyncExecutionRequest<TResult>
    {
        protected AsyncExecutionRequest()
        {

        }

        protected AsyncExecutionRequest(Guid executionId)
            : base(executionId)
        {
        }
    }
}
