using TOS.CQRS.Executions.Events;

namespace TOS.CQRS.Handlers.Events
{
    public interface IAsyncEventHandler<in TEvent> : IAsyncExecutionHandler<TEvent> where TEvent : IAsyncEvent
    {

    }
}
