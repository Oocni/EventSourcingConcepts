using EventSourcingConcepts.Domain.Common.Events;

namespace EventSourcingConcepts.Domain.Thing.ThingEvents;

public sealed record ThingDeleted(int StreamId, DateTime At):IEvent
{
    public int EventOrder => 2;
}