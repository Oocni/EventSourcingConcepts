using EventSourcingConcepts.Stores.Abstraction.Projections;

namespace EventSourcingConcepts.Stores.ProjectionsStore;

public interface IProjectionsStore
{
    void SaveProjection(IProjection projection);
    
    T? GetProjection<T>(int projectionId)
        where T : class, IProjection;
}