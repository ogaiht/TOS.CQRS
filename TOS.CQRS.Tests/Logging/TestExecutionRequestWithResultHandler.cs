using TOS.CQRS.Handlers;

namespace TOS.CQRS.Tests.Logging
{
    public class TestExecutionRequestWithResultHandler : IExecutionHandler<TestExecutionRequestWithResult, string>
    {
        public string Execute(TestExecutionRequestWithResult execution)
        {
            throw new System.NotImplementedException();
        }
    }
}
