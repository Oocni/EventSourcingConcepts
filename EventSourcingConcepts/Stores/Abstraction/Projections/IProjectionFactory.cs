using EventSourcingConcepts.Stores.Abstraction.Events;

namespace EventSourcingConcepts.Stores.Abstraction.Projections;

public interface IProjectionFactory
{
    public IProjection CreateProjection<TProjection>(IEnumerable<IEvent> events, IProjection? projection)
        where TProjection : class, IProjection;
}