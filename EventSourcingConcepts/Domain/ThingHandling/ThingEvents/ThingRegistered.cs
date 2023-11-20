namespace EventSourcingConcepts.Domain.ThingHandling.ThingEvents;

public sealed record ThingRegistered(int Id,
    string ContainerId,
    string ExternalId,
    string Description,
    ThingType Type,
    DateTime At) : IEvent
{
    public int EventOrder => 0;
}