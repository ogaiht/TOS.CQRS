namespace TOS.CQRS.Executions.Commands
{
    public interface IAsyncCommand : IAsyncExecutionRequest
    {

    }

    public interface IAsyncCommand<out result> : IAsyncCommand, IAsyncExecutionRequest<result>
    {

    }
}
