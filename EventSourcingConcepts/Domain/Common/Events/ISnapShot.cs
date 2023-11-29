using EventSourcingConcepts.Domain.Common.Projections;

namespace EventSourcingConcepts.Domain.Common.Events;

public interface ISnapShot
{
    public int StreamId { get; }
    public IProjection Projection { get; }
    public DateTime At { get; }
}