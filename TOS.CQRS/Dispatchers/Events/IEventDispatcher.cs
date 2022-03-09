using System.Threading.Tasks;
using TOS.CQRS.Executions.Events;

namespace TOS.CQRS.Dispatchers.Events
{
    public interface IEventDispatcher
    {
        void Dispatch<TEvent>(TEvent @event) where TEvent : IEvent;

        Task DispatchAsync<TEvent>(TEvent @event) where TEvent : IAsyncEvent;
    }
}
