using System.Collections.Concurrent;
using EventSourcingConcepts.Stores.Abstraction.Events;
using EventSourcingConcepts.Stores.Abstraction.Projections;

namespace EventSourcingConcepts.Stores.EventsStore;

public class EventsStore : IEventsStore
{
    private readonly IProjectionFactory _projectionFactory;
    private readonly IDictionary<int, IList<IEvent>> _streams = new ConcurrentDictionary<int, IList<IEvent>>();
    private readonly IDictionary<int, ISnapShot<IProjection>> _snapShots = new ConcurrentDictionary<int, ISnapShot<IProjection>>();

    public EventsStore(IProjectionFactory projectionFactory)
    {
        _projectionFactory = projectionFactory;
    }
    
    public void AppendToStream(IEvent @event)
    {
        if (!_streams.TryGetValue(@event.StreamId, out var value))
        {
            value = new List<IEvent>();
            _streams.Add(@event.StreamId, value);
        }

        value.Add(@event);
    }

    public IEnumerable<IEvent> LoadEventStream(int streamId)
    {
        return _streams[streamId]
            .OrderBy(e => e.EventOrder)
            .ThenBy(e => e.At)
            .ToList();
    }
    
    public (IEnumerable<IEvent>, ISnapShot<TProjection>?) LoadEventStreamFromSnapShot<TProjection>(int streamId)
        where TProjection : class, IProjection
    {
        List<IEvent> stream;
        if(_snapShots.TryGetValue(streamId, out var snapShot))
        {
            stream = _streams[streamId]
                .Where(e => e.At > snapShot.At)
                .OrderBy(e => e.EventOrder)
                .ThenBy(e => e.At)
                .ToList();
        }
        else
        {
            stream = _streams[streamId]
                .OrderBy(e => e.EventOrder)
                .ThenBy(e => e.At)
                .ToList();
        }

        if (stream.Count <= 500)
        {
            return (stream, (ISnapShot<TProjection>?)snapShot);
        }

        snapShot = new SnapShot<TProjection>(streamId, stream.Last().At, _projectionFactory.CreateProjection<TProjection>(stream, snapShot?.Projection));
        _snapShots[streamId] = snapShot;
        return (Array.Empty<IEvent>(), (ISnapShot<TProjection>?)snapShot);
    }

    public int GetNextStreamId()
    {
        if(_streams.Keys.Count == 0)
            return 1;
        return _streams.Keys.Max() + 1;
    }
}