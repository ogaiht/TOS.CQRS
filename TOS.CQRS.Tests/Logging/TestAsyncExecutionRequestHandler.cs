using System.Threading.Tasks;
using TOS.CQRS.Handlers;

namespace TOS.CQRS.Tests.Logging
{
    public class TestAsyncExecutionRequestHandler : IAsyncExecutionHandler<TestAsyncExecutionRequest>
    {
        public Task ExecuteAsync(TestAsyncExecutionRequest execution)
        {
            throw new System.NotImplementedException();
        }
    }
}
