using System;

namespace TOS.CQRS.Executions.Events
{
    public abstract class Event : IEvent
    {
        protected Event(Guid executionId, DateTime timestamp)
        {
            ExecutionId = executionId;
            Timestamp = timestamp;
        }

        protected Event()
            : this(Guid.NewGuid(), DateTime.UtcNow)
        {

        }

        public Guid ExecutionId { get; }

        public DateTime Timestamp { get; }
    }
}
