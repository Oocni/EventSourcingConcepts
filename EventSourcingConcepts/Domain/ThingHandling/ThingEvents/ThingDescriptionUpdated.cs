namespace EventSourcingConcepts.Domain.ThingHandling.ThingEvents;

public sealed record ThingDescriptionUpdated(int Id,
    string Description, 
    DateTime At):IEvent
{
    public int EventOrder => 1;
}