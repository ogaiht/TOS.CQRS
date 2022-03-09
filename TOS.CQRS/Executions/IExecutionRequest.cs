using System;

namespace TOS.CQRS.Executions
{
    public interface IExecutionRequest
    {
        Guid ExecutionId { get; }
    }

    public interface IExecutionRequest<out TResult> : IExecutionRequest
    {

    }
}
