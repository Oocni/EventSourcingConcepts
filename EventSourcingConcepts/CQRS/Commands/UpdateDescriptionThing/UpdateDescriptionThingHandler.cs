using EventSourcingConcepts.Domain.Thing.ThingEvents;
using EventSourcingConcepts.EventStore;
using MediatR;

namespace EventSourcingConcepts.CQRS.Commands.UpdateDescriptionThing;

public class UpdateDescriptionThingHandler : IRequestHandler<UpdateDescriptionThingCommand>
{
    private readonly IEventStore _eventStore;

    public UpdateDescriptionThingHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }
    
    public Task Handle(UpdateDescriptionThingCommand command, CancellationToken cancellationToken)
    {
        var thingDescriptionUpdated = new ThingDescriptionUpdated(command.ThingId, command.NewDescription, DateTime.UtcNow);
        _eventStore.AppendToStream(thingDescriptionUpdated);
        return Task.CompletedTask;
    }
}