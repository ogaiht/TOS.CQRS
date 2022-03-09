using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TOS.CQRS.Executions;
using TOS.CQRS.Handlers;
using TOS.CQRS.Logging;

namespace TOS.CQRS.Tests.Handlers
{
    [TestFixture]
    public class HandlerExecutorTests
    {
        private Mock<IHandlerExecutorLogger> _handlerExecutorLogger;
        private RequestExecutor _handlerExecutor;

        [SetUp]
        public void SetUp()
        {
            _handlerExecutorLogger = new Mock<IHandlerExecutorLogger>();
            _handlerExecutor = new RequestExecutor(_handlerExecutorLogger.Object);
        }

        [Test]
        public void When_ExecuteRequestWithoutReturn_ShouldLogExecution()
        {
            Mock<IExecutionHandler<TestRequestWithoutReturn>> handler = new Mock<IExecutionHandler<TestRequestWithoutReturn>>();
            TestRequestWithoutReturn request = new TestRequestWithoutReturn();
            Mock<IHandlerExecutorLoggerScope> logScope = new Mock<IHandlerExecutorLoggerScope>();

            _handlerExecutorLogger
                .Setup(l => l.CreateScope(request, handler.Object))
                .Returns(logScope.Object);


            _handlerExecutor.Execute(request, handler.Object);
            handler.Verify(h => h.Execute(request), Times.Once);
            _handlerExecutorLogger.Verify(l => l.CreateScope(request, handler.Object), Times.Once);
            logScope.Verify(l => l.BeforeExecution(), Times.Once);
            logScope.Verify(l => l.TimeExecution(), Times.Once);
            logScope.Verify(l => l.AfterExecution(), Times.Once);
        }

        [Test]
        public void When_ExecuteRequestWithoutReturnWithException_ShouldLogExecutionAndException()
        {
            Mock<IExecutionHandler<TestRequestWithoutReturn>> handler = new Mock<IExecutionHandler<TestRequestWithoutReturn>>();
            TestRequestWithoutReturn request = new TestRequestWithoutReturn();
            Mock<IHandlerExecutorLoggerScope> logScope = new Mock<IHandlerExecutorLoggerScope>();

            _handlerExecutorLogger
                .Setup(l => l.CreateScope(request, handler.Object))
                .Returns(logScope.Object);

            ExecutionTestException exception = new ExecutionTestException();
            handler
                .Setup(h => h.Execute(request))
                .Throws(exception);

            Assert.Throws<ExecutionTestException>(() => _handlerExecutor.Execute(request, handler.Object));

            handler.Verify(h => h.Execute(request), Times.Once);
            _handlerExecutorLogger.Verify(l => l.CreateScope(request, handler.Object), Times.Once);
            logScope.Verify(l => l.BeforeExecution(), Times.Once);
            logScope.Verify(l => l.TimeExecution(), Times.Once);
            logScope.Verify(l => l.AfterExecution(), Times.Never);
            logScope.Verify(l => l.OnError(exception));
        }


        [Test]
        public void When_ExecuteRequestWithReturn_ShouldLogExecutionAndResult()
        {
            const string expectedResult = "Result";

            Mock<IExecutionHandler<TestRequestWithReturn, string>> handler = new Mock<IExecutionHandler<TestRequestWithReturn, string>>();
            TestRequestWithReturn request = new TestRequestWithReturn();
            Mock<IHandlerExecutorLoggerScope<string>> logScope = new Mock<IHandlerExecutorLoggerScope<string>>();

            handler
                .Setup(h => h.Execute(request))
                .Returns(expectedResult);

            _handlerExecutorLogger
                .Setup(l => l.CreateScope<TestRequestWithReturn, IExecutionHandler<TestRequestWithReturn, string>, string>(request, handler.Object))
                .Returns(logScope.Object);


            string result = _handlerExecutor.Execute<TestRequestWithReturn, IExecutionHandler<TestRequestWithReturn, string>, string>(request, handler.Object);

            Assert.AreEqual(expectedResult, result);

            handler.Verify(h => h.Execute(request), Times.Once);
            _handlerExecutorLogger.Verify(l => l.CreateScope<TestRequestWithReturn, IExecutionHandler<TestRequestWithReturn, string>, string>(request, handler.Object), Times.Once);
            logScope.Verify(l => l.BeforeExecution(), Times.Once);
            logScope.Verify(l => l.TimeExecution(), Times.Once);
            logScope.Verify(l => l.AfterExecution(expectedResult), Times.Once);
        }

        [Test]
        public void When_ExecuteRequestWithReturnWithException_ShouldLogExecutionAndException()
        {
            Mock<IExecutionHandler<TestRequestWithReturn, string>> handler = new Mock<IExecutionHandler<TestRequestWithReturn, string>>();
            TestRequestWithReturn request = new TestRequestWithReturn();
            Mock<IHandlerExecutorLoggerScope<string>> logScope = new Mock<IHandlerExecutorLoggerScope<string>>();

            _handlerExecutorLogger
               .Setup(l => l.CreateScope<TestRequestWithReturn, IExecutionHandler<TestRequestWithReturn, string>, string>(request, handler.Object))
               .Returns(logScope.Object);

            ExecutionTestException exception = new ExecutionTestException();
            handler
                .Setup(h => h.Execute(request))
                .Throws(exception);

            Assert.Throws<ExecutionTestException>(() => _handlerExecutor.Execute<TestRequestWithReturn, IExecutionHandler<TestRequestWithReturn, string>, string>(request, handler.Object));

            handler.Verify(h => h.Execute(request), Times.Once);
            _handlerExecutorLogger.Verify(l => l.CreateScope<TestRequestWithReturn, IExecutionHandler<TestRequestWithReturn, string>, string>(request, handler.Object), Times.Once);
            logScope.Verify(l => l.BeforeExecution(), Times.Once);
            logScope.Verify(l => l.TimeExecution(), Times.Once);
            logScope.Verify(l => l.AfterExecution(), Times.Never);
            logScope.Verify(l => l.OnError(exception));
        }


        [Test]
        public async Task When_ExecuteRequestAsyncWithoutReturn_ShouldLogExecution()
        {
            Mock<IAsyncExecutionHandler<TestAsyncRequestWithoutReturn>> handler = new Mock<IAsyncExecutionHandler<TestAsyncRequestWithoutReturn>>();
            TestAsyncRequestWithoutReturn request = new TestAsyncRequestWithoutReturn();
            Mock<IHandlerExecutorLoggerScope> logScope = new Mock<IHandlerExecutorLoggerScope>();

            _handlerExecutorLogger
                .Setup(l => l.CreateScopeForAsync(request, handler.Object))
                .Returns(logScope.Object);


            await _handlerExecutor.ExecuteAsync(request, handler.Object);
            handler.Verify(h => h.ExecuteAsync(request), Times.Once);
            _handlerExecutorLogger.Verify(l => l.CreateScopeForAsync(request, handler.Object), Times.Once);
            logScope.Verify(l => l.BeforeExecution(), Times.Once);
            logScope.Verify(l => l.TimeExecution(), Times.Once);
            logScope.Verify(l => l.AfterExecution(), Times.Once);
        }

        [Test]
        public void When_ExecuteRequestAsyncWithoutReturnWithException_ShouldLogExecutionAndException()
        {
            Mock<IAsyncExecutionHandler<TestAsyncRequestWithoutReturn>> handler = new Mock<IAsyncExecutionHandler<TestAsyncRequestWithoutReturn>>();
            TestAsyncRequestWithoutReturn request = new TestAsyncRequestWithoutReturn();
            Mock<IHandlerExecutorLoggerScope> logScope = new Mock<IHandlerExecutorLoggerScope>();

            _handlerExecutorLogger
                .Setup(l => l.CreateScopeForAsync(request, handler.Object))
                .Returns(logScope.Object);

            ExecutionTestException exception = new ExecutionTestException();
            handler
                .Setup(h => h.ExecuteAsync(request))
                .ThrowsAsync(exception);

            Assert.ThrowsAsync<ExecutionTestException>(async () => await _handlerExecutor.ExecuteAsync(request, handler.Object));

            handler.Verify(h => h.ExecuteAsync(request), Times.Once);
            _handlerExecutorLogger.Verify(l => l.CreateScopeForAsync(request, handler.Object), Times.Once);
            logScope.Verify(l => l.BeforeExecution(), Times.Once);
            logScope.Verify(l => l.TimeExecution(), Times.Once);
            logScope.Verify(l => l.AfterExecution(), Times.Never);
            logScope.Verify(l => l.OnError(exception));
        }


        [Test]
        public async Task When_ExecuteRequestAsyncWithReturn_ShouldLogExecutionAndResult()
        {
            const string expectedResult = "Result";

            Mock<IAsyncExecutionHandler<TestAsyncRequestWithReturn, string>> handler = new Mock<IAsyncExecutionHandler<TestAsyncRequestWithReturn, string>>();
            TestAsyncRequestWithReturn request = new TestAsyncRequestWithReturn();
            Mock<IHandlerExecutorLoggerScope<string>> logScope = new Mock<IHandlerExecutorLoggerScope<string>>();

            handler
                .Setup(h => h.ExecuteAsync(request))
                .ReturnsAsync(expectedResult);

            _handlerExecutorLogger
                .Setup(l => l.CreateScopeForAsync<TestAsyncRequestWithReturn, IAsyncExecutionHandler<TestAsyncRequestWithReturn, string>, string>(request, handler.Object))
                .Returns(logScope.Object);


            string result = await _handlerExecutor.ExecuteAsync<TestAsyncRequestWithReturn, IAsyncExecutionHandler<TestAsyncRequestWithReturn, string>, string>(request, handler.Object);

            Assert.AreEqual(expectedResult, result);

            handler.Verify(h => h.ExecuteAsync(request), Times.Once);
            _handlerExecutorLogger.Verify(l => l.CreateScopeForAsync<TestAsyncRequestWithReturn, IAsyncExecutionHandler<TestAsyncRequestWithReturn, string>, string>(request, handler.Object), Times.Once);
            logScope.Verify(l => l.BeforeExecution(), Times.Once);
            logScope.Verify(l => l.TimeExecution(), Times.Once);
            logScope.Verify(l => l.AfterExecution(expectedResult), Times.Once);
        }

        [Test]
        public void When_ExecuteRequestAsyncWithReturnWithException_ShouldLogExecutionAndException()
        {
            Mock<IAsyncExecutionHandler<TestAsyncRequestWithReturn, string>> handler = new Mock<IAsyncExecutionHandler<TestAsyncRequestWithReturn, string>>();
            TestAsyncRequestWithReturn request = new TestAsyncRequestWithReturn();
            Mock<IHandlerExecutorLoggerScope<string>> logScope = new Mock<IHandlerExecutorLoggerScope<string>>();

            _handlerExecutorLogger
                .Setup(l => l.CreateScopeForAsync<TestAsyncRequestWithReturn, IAsyncExecutionHandler<TestAsyncRequestWithReturn, string>, string>(request, handler.Object))
                .Returns(logScope.Object);

            ExecutionTestException exception = new ExecutionTestException();
            handler
                .Setup(h => h.ExecuteAsync(request))
                .ThrowsAsync(exception);

            Assert.ThrowsAsync<ExecutionTestException>(async () => await _handlerExecutor.ExecuteAsync<TestAsyncRequestWithReturn, IAsyncExecutionHandler<TestAsyncRequestWithReturn, string>, string>(request, handler.Object));

            handler.Verify(h => h.ExecuteAsync(request), Times.Once);
            _handlerExecutorLogger.Verify(l => l.CreateScopeForAsync<TestAsyncRequestWithReturn, IAsyncExecutionHandler<TestAsyncRequestWithReturn, string>, string>(request, handler.Object), Times.Once);
            logScope.Verify(l => l.BeforeExecution(), Times.Once);
            logScope.Verify(l => l.TimeExecution(), Times.Once);
            logScope.Verify(l => l.AfterExecution(), Times.Never);
            logScope.Verify(l => l.OnError(exception));
        }

    }

    public class ExecutionTestException : Exception
    {

    }

    public class TestRequestWithReturn : ExecutionRequest<string>
    {

    }

    public class TestRequestWithoutReturn : ExecutionRequest
    {

    }

    public class TestAsyncRequestWithReturn : AsyncExecutionRequest<string>
    {

    }

    public class TestAsyncRequestWithoutReturn : AsyncExecutionRequest
    {

    }
}
