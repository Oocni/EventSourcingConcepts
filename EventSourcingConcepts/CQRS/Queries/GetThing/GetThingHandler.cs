using EventSourcingConcepts.Domain.Thing;
using EventSourcingConcepts.Stores.ProjectionsStore;
using MediatR;

namespace EventSourcingConcepts.CQRS.Queries.GetThing;

public class GetThingHandler : IRequestHandler<GetThingQuery, ThingProjection?>
{
    private readonly IProjectionsStore _projectionsStore;

    public GetThingHandler(IProjectionsStore projectionsStore)
    {
        _projectionsStore = projectionsStore;
    }
    
    public Task<ThingProjection?> Handle(GetThingQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_projectionsStore.GetProjection<ThingProjection>(request.ThingId));
    }
}