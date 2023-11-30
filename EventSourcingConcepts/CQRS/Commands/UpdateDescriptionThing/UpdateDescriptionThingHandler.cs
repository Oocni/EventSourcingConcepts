using EventSourcingConcepts.Domain.Thing;
using EventSourcingConcepts.Domain.Thing.ThingEvents;
using EventSourcingConcepts.Stores.EventsStore;
using EventSourcingConcepts.Stores.ProjectionsStore;
using MediatR;

namespace EventSourcingConcepts.CQRS.Commands.UpdateDescriptionThing;

public class UpdateDescriptionThingHandler : IRequestHandler<UpdateDescriptionThingCommand>
{
    private readonly IEventsStore _eventsStore;
    private readonly IProjectionsStore _projectionsStore;

    public UpdateDescriptionThingHandler(IEventsStore eventsStore,
        IProjectionsStore projectionsStore)
    {
        _eventsStore = eventsStore;
        _projectionsStore = projectionsStore;
    }
    
    public Task Handle(UpdateDescriptionThingCommand command, CancellationToken cancellationToken)
    {
        var (stream, snapShot) = _eventsStore.LoadEventStreamFromSnapShot<ThingProjection>(command.ThingId);
        var updateDescriptionThingAggregate = UpdateDescriptionThingAggregate.CreateUpdateDescriptionThingAggregate(stream, (ThingProjection?)snapShot?.Projection);
        
        if(updateDescriptionThingAggregate.Description == command.NewDescription || updateDescriptionThingAggregate.State == ThingState.Deleted)
            return Task.CompletedTask;
        
        var thingDescriptionUpdated = new ThingDescriptionUpdated(command.ThingId, command.NewDescription, DateTime.UtcNow);
        _eventsStore.AppendToStream(thingDescriptionUpdated);
        
        (stream, snapShot) = _eventsStore.LoadEventStreamFromSnapShot<ThingProjection>(command.ThingId);
        var thingProjection = new ThingProjection(stream, snapShot?.Projection);
        _projectionsStore.SaveProjection(thingProjection);
        
        return Task.CompletedTask;
    }
}