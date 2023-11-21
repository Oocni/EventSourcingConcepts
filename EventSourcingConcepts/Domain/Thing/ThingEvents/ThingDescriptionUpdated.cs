using EventSourcingConcepts.Domain.Common.Events;

namespace EventSourcingConcepts.Domain.Thing.ThingEvents;

public sealed record ThingDescriptionUpdated(int Id,
    string Description, 
    DateTime At):IEvent
{
    public int EventOrder => 1;
}