using EventSourcingConcepts.Domain.Common.Events;

namespace EventSourcingConcepts.EventStore;

public interface IEventStore
{
    void AppendToStream(IEvent @event);
    IEnumerable<IEvent> LoadEventStream(int streamId);
}