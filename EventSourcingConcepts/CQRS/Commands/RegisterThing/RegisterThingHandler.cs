using EventSourcingConcepts.Domain.Thing;
using EventSourcingConcepts.Domain.Thing.ThingEvents;
using EventSourcingConcepts.Stores.EventsStore;
using EventSourcingConcepts.Stores.ProjectionsStore;
using MediatR;

namespace EventSourcingConcepts.CQRS.Commands.RegisterThing;

public class RegisterThingHandler : IRequestHandler<RegisterThingCommand>
{
    private readonly IEventsStore _eventsStore;
    private readonly IProjectionsStore _projectionsStore;

    public RegisterThingHandler(IEventsStore eventsStore,
        IProjectionsStore projectionsStore)
    {
        _eventsStore = eventsStore;
        _projectionsStore = projectionsStore;
    }
    
    public Task Handle(RegisterThingCommand command, CancellationToken cancellationToken)
    {
        var streamId = _eventsStore.GetNextStreamId();
        var thingRegistered = new ThingRegistered(
            streamId,
            command.ContainerId,
            command.ExternalId,
            command.Description,
            (ThingType)command.Type,
            DateTime.UtcNow);
        _eventsStore.AppendToStream(thingRegistered);

        var stream = _eventsStore.LoadEventStream(streamId);
        var thingProjection = ThingProjection.CreateThing(stream);
        _projectionsStore.SaveProjection(thingProjection);
        
        return Task.CompletedTask;
    }
}