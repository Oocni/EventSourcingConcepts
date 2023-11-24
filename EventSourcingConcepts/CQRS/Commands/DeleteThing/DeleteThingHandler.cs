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
        var stream = _eventsStore.LoadEventStream(command.ThingId);
        var deleteThingAggregate = DeleteThingAggregate.CreateDeleteThingAggregate(stream);
        
        if(deleteThingAggregate.State == ThingState.Deleted)
            return Task.CompletedTask;
        
        var thingDeleted = new ThingDeleted(command.ThingId, DateTime.UtcNow);
        _eventsStore.AppendToStream(thingDeleted);
        
        stream = _eventsStore.LoadEventStream(command.ThingId);
        var thingProjection = ThingProjection.CreateThing(stream);
        _projectionsStore.SaveProjection(thingProjection);
        
        return Task.CompletedTask;
    }
}