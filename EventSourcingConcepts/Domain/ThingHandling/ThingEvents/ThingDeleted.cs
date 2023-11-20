namespace EventSourcingConcepts.Domain.ThingHandling.ThingEvents;

public sealed record ThingDeleted(int Id, DateTime At):IEvent
{
    public int EventOrder => 2;
}