using EventSourcingConcepts.Stores.Abstraction.Events;
using EventSourcingConcepts.Stores.Abstraction.Projections;

namespace EventSourcingConcepts.Stores.EventsStore;

public sealed class SnapShot<TProjection> : ISnapShot<TProjection>
    where TProjection : class, IProjection
{
    public int StreamId { get; set; }
    public DateTime At { get; set; }
    public IProjection Projection { get; set; }

    public SnapShot(int streamId, DateTime at, IProjection projection)
    {
        StreamId = streamId;
        At = at;
        Projection = projection;
    }
}