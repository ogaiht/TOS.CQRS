using System;

namespace TOS.CQRS.Executions.Events
{
    public abstract class AsyncEvent : IAsyncEvent
    {
        protected AsyncEvent(Guid executionId, DateTime timestamp)
        {
            ExecutionId = executionId;
            Timestamp = timestamp;
        }

        protected AsyncEvent()
            : this(Guid.NewGuid(), DateTime.UtcNow)
        {

        }

        public Guid ExecutionId { get; }

        public DateTime Timestamp { get; }
    }
}
