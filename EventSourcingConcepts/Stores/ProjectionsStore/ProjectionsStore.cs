using System.Collections.Concurrent;
using EventSourcingConcepts.Stores.Abstraction.Projections;

namespace EventSourcingConcepts.Stores.ProjectionsStore;

public class ProjectionsStore : IProjectionsStore
{
    private readonly IDictionary<int, IProjection> _projections = new ConcurrentDictionary<int, IProjection>();

    public void SaveProjection(IProjection projection)
    {
        _projections[projection.Id] = projection;
    }

    public T? GetProjection<T>(int projectionId)
        where T : class, IProjection
    {
        if(!_projections.TryGetValue(projectionId, out var value))
            return null;
        return value as T;
    }
}