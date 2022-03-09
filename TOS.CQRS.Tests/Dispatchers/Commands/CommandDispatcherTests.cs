using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TOS.CQRS.Dispatchers;
using TOS.CQRS.Dispatchers.Commands;
using TOS.CQRS.Executions.Commands;
using TOS.CQRS.Handlers;
using TOS.CQRS.Handlers.Commands;

namespace TOS.CQRS.Tests.Dispatchers.Commands
{
    [TestFixture]
    public class CommandDispatcherTests
    {
        private CommandDispatcher _commandDispatcher;
        private Mock<IRequestExecutor> _handlerExecutor;
        private Mock<IExecutionHandlerProvider> _executionHandlerProvider;
        private Mock<ILogger<CommandDispatcher>> _logger;

        [SetUp]
        public void SetUp()
        {
            _handlerExecutor = new Mock<IRequestExecutor>();
            _executionHandlerProvider = new Mock<IExecutionHandlerProvider>();
            _logger = new Mock<ILogger<CommandDispatcher>>();
            _commandDispatcher = new CommandDispatcher(
                _handlerExecutor.Object,
                _executionHandlerProvider.Object,
                _logger.Object);
        }


        [Test]
        public void When_ExecutingCommand_ShouldFindHandler_AndDeliverExecution()
        {
            Mock<ICommand> command = new Mock<ICommand>();
            Mock<ICommandHandler<ICommand>> handler = new Mock<ICommandHandler<ICommand>>();

            _executionHandlerProvider
                .Setup(p => p.GetHandler<ICommand, ICommandHandler<ICommand>>(true))
                .Returns(handler.Object);

            _commandDispatcher.Execute(command.Object);

            _handlerExecutor.Verify(e => e.Execute(command.Object, handler.Object));
        }


        [Test]
        public void When_ExecutingCommandThrowsException_ShouldFindHandler_DeliverExecution_AndLogException()
        {
            Mock<ICommand> command = new Mock<ICommand>();
            Mock<ICommandHandler<ICommand>> handler = new Mock<ICommandHandler<ICommand>>();

            Exception ex = new Exception();

            _handlerExecutor
                .Setup(e => e.Execute(command.Object, handler.Object))
                .Throws(ex);

            _executionHandlerProvider
                .Setup(p => p.GetHandler<ICommand, ICommandHandler<ICommand>>(true))
                .Returns(handler.Object);

            Assert.Throws<Exception>(() => _commandDispatcher.Execute(command.Object));

            _handlerExecutor.Verify(e => e.Execute(command.Object, handler.Object));
        }

        [Test]
        public void When_ExecutingCommandWithResult_ShouldFindHandler_AndDeliverExecution()
        {
            const string expectedResult = "Result";

            Mock<ICommand<string>> command = new Mock<ICommand<string>>();
            Mock<ICommandHandler<ICommand<string>, string>> handler = new Mock<ICommandHandler<ICommand<string>, string>>();

            _executionHandlerProvider
                .Setup(p => p.GetHandler<ICommand<string>, ICommandHandler<ICommand<string>, string>, string>(true))
                .Returns(handler.Object);

            _handlerExecutor
                .Setup(e => e.Execute<ICommand<string>, ICommandHandler<ICommand<string>, string>, string>(command.Object, handler.Object))
                .Returns(expectedResult);

            string actualResult = _commandDispatcher.Execute<ICommand<string>, string>(command.Object);

            Assert.AreEqual(expectedResult, actualResult);

            _handlerExecutor.Verify(e => e.Execute<ICommand<string>, ICommandHandler<ICommand<string>, string>, string>(command.Object, handler.Object));
        }


        [Test]
        public void When_ExecutingCommandWithResultThrowsException_ShouldFindHandler_DeliverExecution_AndLogException()
        {
            Mock<ICommand<string>> command = new Mock<ICommand<string>>();
            Mock<ICommandHandler<ICommand<string>, string>> handler = new Mock<ICommandHandler<ICommand<string>, string>>();

            _executionHandlerProvider
                .Setup(p => p.GetHandler<ICommand<string>, ICommandHandler<ICommand<string>, string>, string>(true))
                .Returns(handler.Object);

            Exception ex = new Exception();

            _handlerExecutor
                .Setup(e => e.Execute<ICommand<string>, ICommandHandler<ICommand<string>, string>, string>(command.Object, handler.Object))
                .Throws(ex);

            Assert.Throws<Exception>(() => _commandDispatcher.Execute<ICommand<string>, string>(command.Object));

            _handlerExecutor.Verify(e => e.Execute<ICommand<string>, ICommandHandler<ICommand<string>, string>, string>(command.Object, handler.Object));
        }

        [Test]
        public async Task When_ExecutingAsyncCommand_ShouldFindHandler_AndDeliverExecution()
        {
            Mock<IAsyncCommand> asyncCommand = new Mock<IAsyncCommand>();
            Mock<IAsyncCommandHandler<IAsyncCommand>> handler = new Mock<IAsyncCommandHandler<IAsyncCommand>>();

            _executionHandlerProvider
                .Setup(p => p.GetAsyncHandler<IAsyncCommand, IAsyncCommandHandler<IAsyncCommand>>(true))
                .Returns(handler.Object);

            await _commandDispatcher.ExecuteAsync(asyncCommand.Object);

            _handlerExecutor.Verify(e => e.ExecuteAsync(asyncCommand.Object, handler.Object));
        }


        [Test]
        public void When_ExecutingAsyncCommandThrowsException_ShouldFindHandler_DeliverExecution_AndLogException()
        {
            Mock<IAsyncCommand> asyncCommand = new Mock<IAsyncCommand>();
            Mock<IAsyncCommandHandler<IAsyncCommand>> handler = new Mock<IAsyncCommandHandler<IAsyncCommand>>();

            Exception ex = new Exception();

            _handlerExecutor
                .Setup(e => e.ExecuteAsync(asyncCommand.Object, handler.Object))
                .ThrowsAsync(ex);

            _executionHandlerProvider
                .Setup(p => p.GetAsyncHandler<IAsyncCommand, IAsyncCommandHandler<IAsyncCommand>>(true))
                .Returns(handler.Object);

            Assert.ThrowsAsync<Exception>(() => _commandDispatcher.ExecuteAsync(asyncCommand.Object));

            _handlerExecutor.Verify(e => e.ExecuteAsync(asyncCommand.Object, handler.Object));
        }

        [Test]
        public async Task When_ExecutingAsyncCommandWithResult_ShouldFindHandler_AndDeliverExecution()
        {
            const string expectedResult = "Result";

            Mock<IAsyncCommand<string>> command = new Mock<IAsyncCommand<string>>();
            Mock<IAsyncCommandHandler<IAsyncCommand<string>, string>> handler = new Mock<IAsyncCommandHandler<IAsyncCommand<string>, string>>();

            _executionHandlerProvider
                .Setup(p => p.GetAsyncHandler<IAsyncCommand<string>, IAsyncCommandHandler<IAsyncCommand<string>, string>, string>(true))
                .Returns(handler.Object);

            _handlerExecutor
                .Setup(e => e.ExecuteAsync<IAsyncCommand<string>, IAsyncCommandHandler<IAsyncCommand<string>, string>, string>(command.Object, handler.Object))
                .ReturnsAsync(expectedResult);

            string actualResult = await _commandDispatcher.ExecuteAsync<IAsyncCommand<string>, string>(command.Object);

            Assert.AreEqual(expectedResult, actualResult);

            _handlerExecutor.Verify(e => e.ExecuteAsync<IAsyncCommand<string>, IAsyncCommandHandler<IAsyncCommand<string>, string>, string>(command.Object, handler.Object));
        }


        [Test]
        public void When_ExecutingAsyncCommandWithResultThrowsException_ShouldFindHandler_DeliverExecution_AndLogException()
        {
            Mock<IAsyncCommand<string>> command = new Mock<IAsyncCommand<string>>();
            Mock<IAsyncCommandHandler<IAsyncCommand<string>, string>> handler = new Mock<IAsyncCommandHandler<IAsyncCommand<string>, string>>();

            _executionHandlerProvider
                .Setup(p => p.GetAsyncHandler<IAsyncCommand<string>, IAsyncCommandHandler<IAsyncCommand<string>, string>, string>(true))
                .Returns(handler.Object);

            Exception ex = new Exception();

            _handlerExecutor
               .Setup(e => e.ExecuteAsync<IAsyncCommand<string>, IAsyncCommandHandler<IAsyncCommand<string>, string>, string>(command.Object, handler.Object))
               .Throws(ex);

            Assert.ThrowsAsync<Exception>(async () => await _commandDispatcher.ExecuteAsync<IAsyncCommand<string>, string>(command.Object));

            _handlerExecutor.Verify(e => e.ExecuteAsync<IAsyncCommand<string>, IAsyncCommandHandler<IAsyncCommand<string>, string>, string>(command.Object, handler.Object));
        }
    }
}
