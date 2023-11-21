using EventSourcingConcepts.Domain.Common.Events;

namespace EventSourcingConcepts.EventStore;

public class EventStore : IEventStore
{
    private readonly IDictionary<int, IList<IEvent>> _streams = new Dictionary<int, IList<IEvent>>();
    
    public void AppendToStream(IEvent @event)
    {
        if (!_streams.ContainsKey(@event.Id))
        {
            _streams.Add(@event.Id, new List<IEvent>());
        }
        _streams[@event.Id].Add(@event);
    }

    public IEnumerable<IEvent> LoadEventStream(int streamId)
    {
        return _streams[streamId]
            .OrderBy(e => e.EventOrder)
            .ThenBy(e => e.At)
            .ToList();
    }
}