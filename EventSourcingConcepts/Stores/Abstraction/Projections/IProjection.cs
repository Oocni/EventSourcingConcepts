using EventSourcingConcepts.Stores.Abstraction.Events;

namespace EventSourcingConcepts.Stores.Abstraction.Projections;

/// <summary>
/// Projection interface
/// Will store a projection of an element from events at a given time
/// </summary>
public interface IProjection
{
    /// <summary>
    /// Id of the element linked to the projection
    /// </summary>
    public int Id { get; }
}