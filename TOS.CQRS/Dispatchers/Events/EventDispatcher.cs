using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TOS.CQRS.Executions.Events;
using TOS.CQRS.Handlers;
using TOS.CQRS.Handlers.Events;

namespace TOS.CQRS.Dispatchers.Events
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IRequestExecutor _handlerExecutor;
        private readonly IExecutionHandlerProvider _executionHandlerProvider;
        private readonly ILogger<EventDispatcher> _logger;

        public EventDispatcher(
            IRequestExecutor handlerExecutor,
            IExecutionHandlerProvider executionHandlerProvider,
            ILogger<EventDispatcher> logger)
        {
            _handlerExecutor = handlerExecutor;
            _executionHandlerProvider = executionHandlerProvider;
            _logger = logger;
        }

        public void Dispatch<TEvent>(TEvent @event) where TEvent : IEvent
        {
            IEnumerable<IEventHandler<TEvent>> handlers = _executionHandlerProvider.GetHandlers<TEvent, IEventHandler<TEvent>>(false);

            if (!handlers.Any())
            {
                return;
            }

            foreach (IEventHandler<TEvent> handler in handlers)
            {
                try
                {
                    _handlerExecutor.Execute(@event, handler);
                }
                catch (Exception ex)
                {
                    LogError<TEvent>(ex, handler);
                    throw;
                }
            }
        }

        public async Task DispatchAsync<TEvent>(TEvent @event) where TEvent : IAsyncEvent
        {
            IEnumerable<IAsyncEventHandler<TEvent>> handlers = _executionHandlerProvider.GetAsyncHandlers<TEvent, IAsyncEventHandler<TEvent>>(false);

            if (!handlers.Any())
            {
                return;
            }

            foreach (IAsyncEventHandler<TEvent> handler in handlers)
            {
                try
                {
                    await _handlerExecutor.ExecuteAsync(@event, handler);
                }
                catch (Exception ex)
                {
                    LogError<TEvent>(ex, handler);
                    throw;
                }
            }
        }

        private void LogError<T>(Exception ex, object handler)
        {
            _logger.LogError(ex, "Error when executing {Event} by handler {Handler}.", typeof(T).FullName, handler.GetType().FullName);
        }
    }
}
