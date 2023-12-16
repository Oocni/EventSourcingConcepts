using EventSourcingConcepts.Stores.Abstraction.Projections;

namespace EventSourcingConcepts.Stores.ProjectionsStore;

/// <summary>
/// Projection store interface
/// </summary>
public interface IProjectionsStore
{
    /// <summary>
    /// Save a projection
    /// </summary>
    /// <param name="projection">
    /// Projection to save
    /// </param>
    void SaveProjection(IProjection projection);
    
    /// <summary>
    /// Get a projection
    /// </summary>
    /// <param name="projectionId">
    /// Projection id
    /// </param>
    /// <typeparam name="T">
    /// Type of the projection
    /// </typeparam>
    /// <returns>
    /// Returns a projection or null if not found
    /// </returns>
    T? GetProjection<T>(int projectionId)
        where T : class, IProjection;
}