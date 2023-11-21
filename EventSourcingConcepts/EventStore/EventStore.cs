using EventSourcingConcepts.Domain.Common.Events;

namespace EventSourcingConcepts.EventStore;

public class EventStore : IEventStore
{
    private readonly IDictionary<int, IList<IEvent>> _streams = new Dictionary<int, IList<IEvent>>();
    
    public void AppendToStream(IEvent @event)
    {
        if (!_streams.ContainsKey(@event.StreamId))
        {
            _streams.Add(@event.StreamId, new List<IEvent>());
        }
        _streams[@event.StreamId].Add(@event);
    }

    public IEnumerable<IEvent> LoadEventStream(int streamId)
    {
        return _streams[streamId]
            .OrderBy(e => e.EventOrder)
            .ThenBy(e => e.At)
            .ToList();
    }

    public int GetNextStreamId()
    {
        if(_streams.Keys.Count == 0)
            return 1;
        return _streams.Keys.Max() + 1;
    }
}