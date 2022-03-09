using TOS.CQRS.Handlers;

namespace TOS.CQRS.Tests.Logging
{
    public class TestExecutionRequestHandler : IExecutionHandler<TestExecutionRequest>
    {
        public void Execute(TestExecutionRequest execution)
        {
            throw new System.NotImplementedException();
        }
    }
}
