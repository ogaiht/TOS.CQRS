using TOS.CQRS.Executions.Events;

namespace TOS.CQRS.Handlers.Events
{
    public interface IEventHandler<in TEvent> : IExecutionHandler<TEvent> where TEvent : IEvent
    {

    }
}
