using EventSourcingConcepts.Domain.Thing;
using EventSourcingConcepts.Domain.Thing.ThingEvents;
using EventSourcingConcepts.Stores.EventsStore;
using EventSourcingConcepts.Stores.ProjectionsStore;
using MediatR;

namespace EventSourcingConcepts.CQRS.Commands.DeleteThing;

public class DeleteThingHandler : IRequestHandler<DeleteThingCommand>
{
    private readonly IEventsStore _eventsStore;
    private readonly IProjectionsStore _projectionsStore;

    public DeleteThingHandler(IEventsStore eventsStore,
        IProjectionsStore projectionsStore)
    {
        _eventsStore = eventsStore;
        _projectionsStore = projectionsStore;
    }
    
    public Task Handle(DeleteThingCommand command, CancellationToken cancellationToken)
    {
        var (stream, snapShot) = _eventsStore.LoadEventStreamFromSnapShot<ThingProjection>(command.ThingId);
        var deleteThingAggregate = DeleteThingAggregate.CreateDeleteThingAggregate(stream, (ThingProjection?)snapShot?.Projection);
        
        if(deleteThingAggregate.State == ThingState.Deleted)
            return Task.CompletedTask;
        
        var thingDeleted = new ThingDeleted(command.ThingId, DateTime.UtcNow);
        _eventsStore.AppendToStream(thingDeleted);
        
        (stream, snapShot) = _eventsStore.LoadEventStreamFromSnapShot<ThingProjection>(command.ThingId);
        var thingProjection = new ThingProjection(stream, snapShot?.Projection);
        _projectionsStore.SaveProjection(thingProjection);
        
        return Task.CompletedTask;
    }
}