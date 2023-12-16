using EventSourcingConcepts.Stores.Abstraction.Events;
using EventSourcingConcepts.Stores.Abstraction.Projections;

namespace EventSourcingConcepts.Stores.EventsStore;

/// <summary>
/// Events store interface
/// Will provide methods to append events to a stream and load events from a stream
/// </summary>
public interface IEventsStore
{
    /// <summary>
    /// Append an event to a stream
    /// </summary>
    /// <param name="event">
    /// Event to append
    /// </param>
    void AppendToStream(IEvent @event);
    
    /// <summary>
    /// Load an event stream
    /// </summary>
    /// <param name="streamId">
    /// Stream id
    /// Id of the object linked to the stream
    /// </param>
    /// <returns>
    /// Returns a list of events linked to the stream id
    /// </returns>
    IEnumerable<IEvent> LoadEventStream(int streamId);
    
    /// <summary>
    /// Load an event stream
    /// If a snapshot is found, it will return the snapshot and the next events
    /// If no snapshot is found, it will return all the events
    /// </summary>
    /// <param name="streamId">
    /// Stream id
    /// Id of the object linked to the stream
    /// </param>
    /// <typeparam name="TProjection">
    /// Type of the projection
    /// </typeparam>
    /// <returns>
    /// Returns a list of events linked to the stream id and a snapshot if found
    /// </returns>
    (IEnumerable<IEvent>, ISnapShot<TProjection>?) LoadEventStreamFromSnapShot<TProjection>(int streamId)
        where TProjection : class, IProjection;
    
    /// <summary>
    /// Get the next stream id
    /// </summary>
    /// <returns>
    /// Returns the next stream id
    /// </returns>
    int GetNextStreamId();
}