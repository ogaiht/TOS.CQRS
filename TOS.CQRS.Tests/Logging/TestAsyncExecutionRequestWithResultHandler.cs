using System.Threading.Tasks;
using TOS.CQRS.Handlers;

namespace TOS.CQRS.Tests.Logging
{
    public class TestAsyncExecutionRequestWithResultHandler : IAsyncExecutionHandler<TestAsyncExecutionRequestWithResult, string>
    {
        public Task<string> ExecuteAsync(TestAsyncExecutionRequestWithResult execution)
        {
            throw new System.NotImplementedException();
        }
    }
}
