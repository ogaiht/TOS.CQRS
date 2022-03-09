using System;

namespace TOS.CQRS.Executions.Events
{
    public interface IAsyncEvent : IAsyncExecutionRequest
    {
        DateTime Timestamp { get; }
    }
}
