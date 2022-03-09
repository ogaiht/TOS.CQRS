namespace TOS.CQRS.Executions
{
    public interface IAsyncExecutionRequest : IExecutionRequest
    {

    }

    public interface IAsyncExecutionRequest<out TResult> : IAsyncExecutionRequest
    {

    }
}
