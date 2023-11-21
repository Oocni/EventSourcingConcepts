using EventSourcingConcepts.Domain.Common.Events;

namespace EventSourcingConcepts.Domain.Thing.ThingEvents;

public sealed record ThingRegistered(int Id,
    string ContainerId,
    string ExternalId,
    string Description,
    ThingType Type,
    DateTime At) : IEvent
{
    public int EventOrder => 0;
}