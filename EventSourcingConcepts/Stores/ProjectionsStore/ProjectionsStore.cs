using EventSourcingConcepts.Domain.Common.Projections;

namespace EventSourcingConcepts.Stores.ProjectionsStore;

public class ProjectionsStore : IProjectionsStore
{
    private readonly IDictionary<int, IProjection> _projections = new Dictionary<int, IProjection>();

    public void SaveProjection(IProjection projection)
    {
        _projections[projection.Id] = projection;
    }

    public T? GetProjection<T>(int projectionId)
        where T : class, IProjection
    {
        if(!_projections.ContainsKey(projectionId))
            return null;
        return _projections[projectionId] as T;
    }
}