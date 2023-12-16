namespace EventSourcingConcepts.Stores.Abstraction.Events;

/// <summary>
/// Event interface
/// </summary>
public interface IEvent
{
    /// <summary>
    /// Event order in the stream
    /// Even if the event is received out of order, the event store will order the events
    /// </summary>
    int EventOrder { get; }
    
    /// <summary>
    /// Stream id
    /// Id of the object linked to the event
    /// </summary>
    int StreamId { get; }
    
    /// <summary>
    /// Event date time
    /// </summary>
    DateTime At { get; }
}