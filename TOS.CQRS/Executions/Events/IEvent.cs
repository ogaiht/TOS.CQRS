using System;

namespace TOS.CQRS.Executions.Events
{
    public interface IEvent : IExecutionRequest
    {
        DateTime Timestamp { get; }
    }
}
