namespace TOS.CQRS.Executions.Commands
{
    public interface ICommand : IExecutionRequest
    {

    }

    public interface ICommand<out result> : ICommand, IExecutionRequest<result>
    {

    }
}
