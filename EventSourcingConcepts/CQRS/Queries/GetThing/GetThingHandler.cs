using EventSourcingConcepts.Domain.Common.Events;
using EventSourcingConcepts.Domain.Thing;
using EventSourcingConcepts.Domain.Thing.ThingEvents;
using EventSourcingConcepts.EventStore;
using MediatR;

namespace EventSourcingConcepts.CQRS.Queries.GetThing;

public class GetThingHandler : IRequestHandler<GetThingQuery, Thing>
{
    private readonly IEventStore _eventStore;

    public GetThingHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }
    
    public Task<Thing> Handle(GetThingQuery request, CancellationToken cancellationToken)
    {
        var stream = _eventStore.LoadEventStream(request.ThingId);
        return Task.FromResult(ConstructThing(stream));
    }
    
    private static Thing ConstructThing(IEnumerable<IEvent> stream)
    {
        var thing = new Thing();
    
        foreach (var @event in stream)
        {
            switch (@event)
            {
                case ThingRegistered thingRegistered:
                    thing.Id = thingRegistered.StreamId;
                    thing.ContainerId = thingRegistered.ContainerId;
                    thing.ExternalId = thingRegistered.ExternalId;
                    thing.Description = thingRegistered.Description;
                    thing.Type = thingRegistered.Type;
                    break;
                case ThingDescriptionUpdated thingDescriptionUpdated:
                    thing.Description = thingDescriptionUpdated.Description;
                    break;
                case ThingDeleted:
                    thing.State = ThingState.Deleted;
                    break;
            }
        }

        return thing;
    }
}