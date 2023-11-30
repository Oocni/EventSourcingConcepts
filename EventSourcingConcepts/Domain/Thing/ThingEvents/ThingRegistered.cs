using EventSourcingConcepts.Stores.Abstraction.Events;

namespace EventSourcingConcepts.Domain.Thing.ThingEvents;

public sealed record ThingRegistered(int StreamId,
    string ContainerId,
    string ExternalId,
    string Description,
    ThingType Type,
    DateTime At) : IEvent
{
    public int EventOrder => 0;
}