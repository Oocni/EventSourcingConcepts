using EventSourcingConcepts.Domain.Common.Events;

namespace EventSourcingConcepts.Stores.EventsStore;

public interface IEventsStore
{
    void AppendToStream(IEvent @event);
    IEnumerable<IEvent> LoadEventStream(int streamId);
    int GetNextStreamId();
}