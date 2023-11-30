using EventSourcingConcepts.Domain.Thing;
using EventSourcingConcepts.Stores.Abstraction.Events;
using EventSourcingConcepts.Stores.Abstraction.Projections;

namespace EventSourcingConcepts.Domain;

public class ProjectionFactory : IProjectionFactory
{
    public IProjection CreateProjection<TProjection>(IEnumerable<IEvent> events, IProjection? projection)
        where TProjection : class, IProjection
    {
        return typeof(TProjection) switch
        {
            var type when type == typeof(ThingProjection) => new ThingProjection(events, projection),
            _ => throw new ArgumentException($"Projection {typeof(IProjection)} not supported")
        };
    }
}