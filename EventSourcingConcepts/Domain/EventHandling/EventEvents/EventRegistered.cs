namespace EventSourcingConcepts.Domain.EventHandling.EventEvents;

public record EventRegistered(int Id,
    string ContainerId,
    string ExternalId,
    string Description,
    EventType Type,
    DateTime RegisteredAt);