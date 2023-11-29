using EventSourcingConcepts.Domain.Common.Events;
using EventSourcingConcepts.Domain.Common.Projections;

namespace EventSourcingConcepts.Domain.Thing;

public sealed class ThingSnapShot : ISnapShot
{
    public int StreamId { get; set; }
    public DateTime At { get; set; }
    public IProjection Projection { get; set; }

    public ThingSnapShot(int streamId, DateTime at, IProjection projection)
    {
        StreamId = streamId;
        At = at;
        Projection = projection;
    }
}