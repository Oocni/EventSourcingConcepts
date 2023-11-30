using EventSourcingConcepts.Stores.Abstraction.Events;

namespace EventSourcingConcepts.Stores.Abstraction.Projections;

public interface IProjection
{
    public int Id { get; }
}