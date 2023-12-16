using EventSourcingConcepts.Stores.Abstraction.Projections;

namespace EventSourcingConcepts.Stores.Abstraction.Events;

/// <summary>
/// Snapshot interface
/// It will store a projection of an element at a given time
/// All new projections and aggregates will be created from the last snapshot + next events
/// </summary>
/// <typeparam name="TProjection">
/// Type of the projection
/// </typeparam>
public interface ISnapShot<out TProjection>
    where TProjection : IProjection
{
    /// <summary>
    /// Stream id
    /// Id of the object linked to the event 
    /// </summary>
    public int StreamId { get; }
    
    /// <summary>
    /// Snapshot projection
    /// </summary>
    public IProjection Projection { get; }
    
    /// <summary>
    /// Snapshot date time
    /// </summary>
    public DateTime At { get; }
}