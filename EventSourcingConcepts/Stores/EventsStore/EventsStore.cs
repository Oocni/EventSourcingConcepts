using System.Collections.Concurrent;
using EventSourcingConcepts.Domain.Common.Events;
using EventSourcingConcepts.Domain.Thing;

namespace EventSourcingConcepts.Stores.EventsStore;

public class EventsStore : IEventsStore
{
    private readonly IDictionary<int, IList<IEvent>> _streams = new ConcurrentDictionary<int, IList<IEvent>>();
    private readonly IDictionary<int, ISnapShot> _snapShots = new ConcurrentDictionary<int, ISnapShot>();
    
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
    
    public (IEnumerable<IEvent>, ISnapShot?) LoadEventStreamFromSnapShot(int streamId)
    {
        if(_snapShots.TryGetValue(streamId, out var snapShot))
        {
            //TODO: Faire le snap shot au delà de 500 events ici aussi
            var events = _streams[streamId]
                .Where(e => e.At > snapShot.At)
                .OrderBy(e => e.EventOrder)
                .ThenBy(e => e.At)
                .ToList();
            return (events, snapShot);
        }
        
        var stream = _streams[streamId]
            .OrderBy(e => e.EventOrder)
            .ThenBy(e => e.At)
            .ToList();

        if (stream.Count > 500)
        {
            var thingSnapShot = new ThingSnapShot(streamId, stream.Last().At, ThingProjection.CreateThing(stream, null));
            _snapShots[streamId] = thingSnapShot;
            return (Array.Empty<IEvent>(), thingSnapShot);
        }

        return (stream, null);
    }

    public int GetNextStreamId()
    {
        if(_streams.Keys.Count == 0)
            return 1;
        return _streams.Keys.Max() + 1;
    }
}