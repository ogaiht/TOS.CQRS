using NUnit.Framework;
using TOS.CQRS.Executions;
using TOS.CQRS.Logging;

namespace TOS.CQRS.Tests.Logging
{
    [TestFixture]
    public class HandlerExecutorIdGeneratorTests
    {
        private HandlerExecutorIdGenerator _handlerExecutorIdGenerator = new HandlerExecutorIdGenerator();

        [Test]
        public void CreateExecutionId_WhenCreatingExecutionId_ShouldUseHandlerAndExecutinTypeAndExecutionId()
        {
            TestExecutionRequest request = new TestExecutionRequest();
            TestExecutionRequestHandler handler = new TestExecutionRequestHandler();
            string expectedExecutionId = CreateExpectedId(handler, request);
            string actualExecutionId = _handlerExecutorIdGenerator.CreateExecutionId(request, handler);
            Assert.AreEqual(expectedExecutionId, actualExecutionId);
        }

        [Test]
        public void CreateExecutionId_WhenCreatingExecutionIdForExecutionWithResult_ShouldUseHandlerAndExecutinTypeAndExecutionId()
        {
            TestExecutionRequestWithResult request = new TestExecutionRequestWithResult();
            TestExecutionRequestWithResultHandler handler = new TestExecutionRequestWithResultHandler();
            string expectedExecutionId = CreateExpectedId(handler, request);
            string actualExecutionId = _handlerExecutorIdGenerator.CreateExecutionId<TestExecutionRequestWithResult, TestExecutionRequestWithResultHandler, string>(request, handler);
            Assert.AreEqual(expectedExecutionId, actualExecutionId);
        }

        [Test]
        public void CreateExecutionId_WhenCreatingAsyncExecutionId_ShouldUseHandlerAndExecutinTypeAndExecutionId()
        {
            TestAsyncExecutionRequest request = new TestAsyncExecutionRequest();
            TestAsyncExecutionRequestHandler handler = new TestAsyncExecutionRequestHandler();
            string expectedExecutionId = CreateExpectedId(handler, request);
            string actualExecutionId = _handlerExecutorIdGenerator.CreateExecutionIdForAsync(request, handler);
            Assert.AreEqual(expectedExecutionId, actualExecutionId);
        }

        [Test]
        public void CreateExecutionId_WhenCreatingAsyncExecutionIdForExecutionWithResult_ShouldUseHandlerAndExecutinTypeAndExecutionId()
        {
            TestAsyncExecutionRequestWithResult request = new TestAsyncExecutionRequestWithResult();
            TestAsyncExecutionRequestWithResultHandler handler = new TestAsyncExecutionRequestWithResultHandler();
            string expectedExecutionId = CreateExpectedId(handler, request);
            string actualExecutionId = _handlerExecutorIdGenerator.CreateExecutionIdForAsync<TestAsyncExecutionRequestWithResult, TestAsyncExecutionRequestWithResultHandler, string>(request, handler);
            Assert.AreEqual(expectedExecutionId, actualExecutionId);
        }

        private static string CreateExpectedId(object handler, IExecutionRequest execution)
        {
            return $"{handler.GetType().FullName}:{execution.GetType().FullName}:{execution.ExecutionId}";
        }
    }
}
