using EventSourcingConcepts.Stores.Abstraction.Events;

namespace EventSourcingConcepts.Domain.Thing.ThingEvents;

public sealed record ThingDescriptionUpdated(int StreamId,
    string Description, 
    DateTime At):IEvent
{
    public int EventOrder => 1;
}