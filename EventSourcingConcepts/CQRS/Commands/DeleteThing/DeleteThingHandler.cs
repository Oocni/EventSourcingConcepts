using EventSourcingConcepts.Domain.Thing;
using EventSourcingConcepts.Stores.Abstraction.Events;
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
        var deleteThingAggregate = new DeleteThingAggregate((IReadOnlyCollection<IEvent>)stream, (ThingProjection?)snapShot?.Projection);
        
        if(!deleteThingAggregate.CanExecute())
            return Task.CompletedTask;
        
        deleteThingAggregate.Execute();
        var uncommittedEvents = deleteThingAggregate.GetUncommittedEvents();

        if(!uncommittedEvents.Any())
            return Task.CompletedTask;
            
        foreach (var uncommittedEvent in uncommittedEvents)
        {
            _eventsStore.AppendToStream(uncommittedEvent);    
        }
        
        (stream, snapShot) = _eventsStore.LoadEventStreamFromSnapShot<ThingProjection>(command.ThingId);
        var thingProjection = new ThingProjection(stream, snapShot?.Projection);
        _projectionsStore.SaveProjection(thingProjection);
        
        return Task.CompletedTask;
    }
}