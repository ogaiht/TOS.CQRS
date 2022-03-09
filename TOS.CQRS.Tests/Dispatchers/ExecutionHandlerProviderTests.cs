using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TOS.Common.Utils;
using TOS.CQRS.Dispatchers;
using TOS.CQRS.Executions;
using TOS.CQRS.Handlers;

namespace TOS.CQRS.Tests.Dispatchers
{
    [TestFixture]
    public class ExecutionHandlerProviderTests
    {
        private Mock<IServiceProvider> _serviceProvider;
        private Mock<IExceptionHelper> _exceptionHelper;
        private Mock<ILogger<ExecutionHandlerProvider>> _logger;
        private ExecutionHandlerProvider _executionHandlerProvider;

        [SetUp]
        public void SetUp()
        {
            _serviceProvider = new Mock<IServiceProvider>();
            _exceptionHelper = new Mock<IExceptionHelper>();
            _logger = new Mock<ILogger<ExecutionHandlerProvider>>();
            _executionHandlerProvider = new ExecutionHandlerProvider(
                _serviceProvider.Object,
                _exceptionHelper.Object,
                _logger.Object);
        }

        [Test]
        [TestCase(true, true)]
        [TestCase(false, true)]
        [TestCase(true, false)]
        [TestCase(false, false)]
        public void GetHandlerFor_WhenFindingHandler_ShouldValidateForNotFound_AndReturnIfFound(bool serviceFound, bool throwIfNotFound)
        {
            TestHandler1 expectedTestHandler = new TestHandler1();
            if (serviceFound)
            {
                _serviceProvider
                .Setup(p => p.GetService(typeof(ITestHandler)))
                .Returns(expectedTestHandler);
            }

            IExecutionHandler<IExecutionRequest> actualTestService = _executionHandlerProvider.GetHandler<IExecutionRequest, ITestHandler>(throwIfNotFound);
            if (serviceFound)
            {
                Assert.AreEqual(expectedTestHandler, actualTestService);
            }
            else
            {
                Assert.IsNull(actualTestService);
            }
            _exceptionHelper
                .Verify(e => e.CheckInvalidOperationException(!serviceFound && throwIfNotFound, "No handler was found for " + typeof(ITestHandler).FullName));
        }

        [Test]
        [TestCase(true, true)]
        [TestCase(false, true)]
        [TestCase(true, false)]
        [TestCase(false, false)]
        public void GetHandlerFor_WhenFindingHandlers_ShouldValidateForNotFound_AndReturnIfFound(bool serviceFound, bool throwIfNotFound)
        {
            TestHandler1 expectedTestHandler1 = new TestHandler1();
            TestHandler2 expectedTestHandler2 = new TestHandler2();
            if (serviceFound)
            {
                _serviceProvider
                .Setup(p => p.GetService(typeof(IEnumerable<ITestHandler>)))
                .Returns(new ITestHandler[] { expectedTestHandler1, expectedTestHandler2 });
            }

            if (serviceFound)
            {
                IEnumerable<ITestHandler> actualServices = _executionHandlerProvider.GetHandlers<IExecutionRequest, ITestHandler>(throwIfNotFound);
                CollectionAssert.Contains(actualServices, expectedTestHandler1);
                CollectionAssert.Contains(actualServices, expectedTestHandler2);
                _exceptionHelper
                    .Verify(e => e.CheckInvalidOperationException(!serviceFound && throwIfNotFound, "No handler was found for " + typeof(ITestHandler).FullName));
            }
            else
            {
                Assert.Throws<InvalidOperationException>(() => _executionHandlerProvider.GetHandlers<IExecutionRequest, ITestHandler>(throwIfNotFound), "No service for type 'System.Collections.Generic.IEnumerable`1[TOS.CQRS.Tests.Dispatchers.ITestService]' has been registered");
            }
        }
    }

    public interface ITestHandler : IExecutionHandler<IExecutionRequest>
    {

    }

    public class TestHandler1 : ITestHandler
    {
        public void Execute(IExecutionRequest execution)
        {
            throw new NotImplementedException();
        }
    }

    public class TestHandler2 : ITestHandler
    {
        public void Execute(IExecutionRequest execution)
        {
            throw new NotImplementedException();
        }
    }
}
