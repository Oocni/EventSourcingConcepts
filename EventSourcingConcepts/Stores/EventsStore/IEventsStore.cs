using EventSourcingConcepts.Stores.Abstraction.Events;
using EventSourcingConcepts.Stores.Abstraction.Projections;

namespace EventSourcingConcepts.Stores.EventsStore;

public interface IEventsStore
{
    void AppendToStream(IEvent @event);
    IEnumerable<IEvent> LoadEventStream(int streamId);
    (IEnumerable<IEvent>, ISnapShot<TProjection>?) LoadEventStreamFromSnapShot<TProjection>(int streamId)
        where TProjection : class, IProjection;
    int GetNextStreamId();
}