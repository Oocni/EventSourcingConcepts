using EventSourcingConcepts.Domain.Thing;
using EventSourcingConcepts.Domain.Thing.ThingEvents;
using EventSourcingConcepts.EventStore;
using MediatR;

namespace EventSourcingConcepts.CQRS.Commands.RegisterThing;

public class RegisterThingHandler : IRequestHandler<RegisterThingCommand>
{
    private readonly IEventStore _eventStore;

    public RegisterThingHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }
    
    public Task Handle(RegisterThingCommand request, CancellationToken cancellationToken)
    {
        var streamId = _eventStore.GetNextStreamId();
        var thingRegistered = new ThingRegistered(
            streamId,
            request.ContainerId,
            request.ExternalId,
            request.Description,
            (ThingType)request.Type,
            DateTime.UtcNow);
        _eventStore.AppendToStream(thingRegistered);
        return Task.CompletedTask;
    }
}