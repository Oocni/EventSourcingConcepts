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
        var (stream, snapShot) = _eventsStore.LoadEventStreamFromSnapShot(command.ThingId);
        var deleteThingAggregate = DeleteThingAggregate.CreateDeleteThingAggregate(stream, snapShot as ThingSnapShot);
        
        if(deleteThingAggregate.State == ThingState.Deleted)
            return Task.CompletedTask;
        
        var thingDeleted = new ThingDeleted(command.ThingId, DateTime.UtcNow);
        _eventsStore.AppendToStream(thingDeleted);
        
        (stream, snapShot) = _eventsStore.LoadEventStreamFromSnapShot(command.ThingId);
        var thingProjection = ThingProjection.CreateThing(stream, snapShot as ThingSnapShot);
        _projectionsStore.SaveProjection(thingProjection);
        
        return Task.CompletedTask;
    }
}