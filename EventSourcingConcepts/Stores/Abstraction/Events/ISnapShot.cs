using EventSourcingConcepts.Stores.Abstraction.Projections;

namespace EventSourcingConcepts.Stores.Abstraction.Events;

public interface ISnapShot<out TProjection>
    where TProjection : IProjection
{
    public int StreamId { get; }
    public IProjection Projection { get; }
    public DateTime At { get; }
}