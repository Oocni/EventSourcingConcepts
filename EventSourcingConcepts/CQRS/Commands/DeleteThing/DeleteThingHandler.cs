using EventSourcingConcepts.Domain.Thing.ThingEvents;
using EventSourcingConcepts.EventStore;
using MediatR;

namespace EventSourcingConcepts.CQRS.Commands.DeleteThing;

public class DeleteThingHandler : IRequestHandler<DeleteThingCommand>
{
    private readonly IEventStore _eventStore;

    public DeleteThingHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }
    
    public Task Handle(DeleteThingCommand command, CancellationToken cancellationToken)
    {
        var thingDeleted = new ThingDeleted(command.ThingId, DateTime.UtcNow);
        _eventStore.AppendToStream(thingDeleted);
        return Task.CompletedTask;
    }
}