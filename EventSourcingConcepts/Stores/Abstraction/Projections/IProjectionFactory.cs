using EventSourcingConcepts.Stores.Abstraction.Events;

namespace EventSourcingConcepts.Stores.Abstraction.Projections;

/// <summary>
/// Projection factory interface
/// Will provide a projection from a list of events and a previous projection from a snapshot
/// </summary>
public interface IProjectionFactory
{
    /// <summary>
    /// Method to create a projection from a list of events and a previous projection from a snapshot
    /// </summary>
    /// <param name="events">
    /// List of events
    /// </param>
    /// <param name="projection">
    /// Previous projection from a snapshot
    /// </param>
    /// <typeparam name="TProjection">
    /// Type of the projection
    /// </typeparam>
    /// <returns>
    /// Returns a projection
    /// </returns>
    public IProjection CreateProjection<TProjection>(IEnumerable<IEvent> events, IProjection? projection)
        where TProjection : class, IProjection;
}