namespace EventSourcingConcepts.Domain.EventHandling.EventEvents;

public record EventDeleted(int Id, DateTime DeletedAt);