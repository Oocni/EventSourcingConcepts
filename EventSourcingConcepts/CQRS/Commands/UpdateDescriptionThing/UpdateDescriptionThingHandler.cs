using EventSourcingConcepts.Domain.Thing;
using EventSourcingConcepts.Stores.Abstraction.Events;
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
        var updateDescriptionThingAggregate = new UpdateDescriptionThingAggregate((IReadOnlyCollection<IEvent>)stream, (ThingProjection?)snapShot?.Projection);
        
        if(!updateDescriptionThingAggregate.CanExecute(command.NewDescription))
            return Task.CompletedTask;
        
        updateDescriptionThingAggregate.Execute(command.NewDescription);
        var uncommittedEvents = updateDescriptionThingAggregate.GetUncommittedEvents();
        
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