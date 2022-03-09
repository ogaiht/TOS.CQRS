//using Microsoft.Extensions.Logging;
//using Moq;
//using NUnit.Framework;
//using TOS.Common.Serialization.Json;
//using TOS.CQRS.Handlers;
//using TOS.CQRS.Logging;

//namespace TOS.CQRS.Tests.Logging
//{

//    [TestFixture]
//    public class HandlerExecutorLoggerTests
//    {
//        private Mock<IJsonSerializer> _jsonSerializer;
//        private Mock<ILogger<HandlerExecutor>> _logger;
//        private Mock<IHandlerExecutorIdGenerator> _handlerExecutorIdGenerator;
//        private HandlerExecutorLogger _handlerExecutorLogger;

//        [SetUp]
//        public void SetUp()
//        {
//            _jsonSerializer = new Mock<IJsonSerializer>();
//            _logger = new Mock<ILogger<HandlerExecutor>>();
//            _handlerExecutorIdGenerator = new Mock<IHandlerExecutorIdGenerator>();
//            _handlerExecutorLogger = new HandlerExecutorLogger(
//                _jsonSerializer.Object,
//                _logger.Object,
//                _handlerExecutorIdGenerator.Object);
//        }

//        [Test]
//        [TestCase(true)]
//        [TestCase(false)]
//        public void Test(bool debugEnabled)
//        {
//            const string identity = "execution-identity";
//            const string serializedRequest = "text execution request";
//            TestExecutionRequest request = new TestExecutionRequest();
//            TestExecutionRequestHandler handler = new TestExecutionRequestHandler();

//            _jsonSerializer
//                .Setup(s => s.Serialize(request))
//                .Returns(serializedRequest);
//            _handlerExecutorIdGenerator
//                .Setup(g => g.CreateExecutionId(request, handler))
//                .Returns(identity);

//            using (IHandlerExecutorLoggerScope scope = _handlerExecutorLogger.CreateScope(request, handler))
//            {
//                scope.BeforeExecution();
//                using (scope.TimeExecution())
//                {

//                }
//                scope.AfterExecution();
//            }
//            _logger
//                .Verify(l => l.Log(LogLevel.Information, "Executing {identity}.", identity));
//            if (debugEnabled)
//            {
//                _logger
//                    .Verify(l => l.Log(LogLevel.Debug, "Executing {identity} for request '{request}'.", serializedRequest));
//            }
//        }
//    }
//}
