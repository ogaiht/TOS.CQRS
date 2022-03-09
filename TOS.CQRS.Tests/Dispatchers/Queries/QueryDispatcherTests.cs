using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using TOS.CQRS.Dispatchers;
using TOS.CQRS.Dispatchers.Queries;
using TOS.CQRS.Executions.Queries;
using TOS.CQRS.Handlers;
using TOS.CQRS.Handlers.Queries;

namespace TOS.CQRS.Tests.Dispatchers.Queries
{
    [TestFixture]
    public class QueryDispatcherTests
    {
        private Mock<IRequestExecutor> _handlerExecutor;
        private Mock<IExecutionHandlerProvider> _executionHandlerProvider;
        private Mock<ILogger<QueryDispatcher>> _logger;
        private QueryDispatcher _queryDispatcher;

        [SetUp]
        public void SetUp()
        {
            _handlerExecutor = new Mock<IRequestExecutor>();
            _executionHandlerProvider = new Mock<IExecutionHandlerProvider>();
            _logger = new Mock<ILogger<QueryDispatcher>>();
            _queryDispatcher = new QueryDispatcher(
                _handlerExecutor.Object,
                _executionHandlerProvider.Object,
                _logger.Object);
        }


        [Test]
        public void When_ExecutingQuery_ShouldFindHandler_AndDeliverExecution()
        {
            const string expectedResult = "Result";

            Mock<IQuery<string>> query = new Mock<IQuery<string>>();
            Mock<IQueryHandler<IQuery<string>, string>> handler = new Mock<IQueryHandler<IQuery<string>, string>>();

            _executionHandlerProvider
                .Setup(p => p.GetHandler<IQuery<string>, IQueryHandler<IQuery<string>, string>, string>(true))
                .Returns(handler.Object);

            _handlerExecutor
                .Setup(e => e.Execute<IQuery<string>, IQueryHandler<IQuery<string>, string>, string>(query.Object, handler.Object))
                .Returns(expectedResult);

            string actualResult = _queryDispatcher.Execute<IQuery<string>, string>(query.Object);

            Assert.AreEqual(expectedResult, actualResult);

            _handlerExecutor.Verify(e => e.Execute<IQuery<string>, IQueryHandler<IQuery<string>, string>, string>(query.Object, handler.Object));
        }

        [Test]
        public async Task When_ExecutingAsyncQuery_ShouldFindHandler_AndDeliverExecution()
        {
            const string expectedResult = "Result";

            Mock<IAsyncQuery<string>> asynQuery = new Mock<IAsyncQuery<string>>();
            Mock<IAsyncQueryHandler<IAsyncQuery<string>, string>> handler = new Mock<IAsyncQueryHandler<IAsyncQuery<string>, string>>();

            _executionHandlerProvider
                .Setup(p => p.GetAsyncHandler<IAsyncQuery<string>, IAsyncQueryHandler<IAsyncQuery<string>, string>, string>(true))
                .Returns(handler.Object);

            _handlerExecutor
                .Setup(e => e.ExecuteAsync<IAsyncQuery<string>, IAsyncQueryHandler<IAsyncQuery<string>, string>, string>(asynQuery.Object, handler.Object))
                .ReturnsAsync(expectedResult);

            string actualResult = await _queryDispatcher.ExecuteAsync<IAsyncQuery<string>, string>(asynQuery.Object);

            Assert.AreEqual(expectedResult, actualResult);

            _handlerExecutor.Verify(e => e.ExecuteAsync<IAsyncQuery<string>, IAsyncQueryHandler<IAsyncQuery<string>, string>, string>(asynQuery.Object, handler.Object));
        }

    }
}
