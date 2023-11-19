namespace EventSourcingConcepts.Domain.EventHandling.EventEvents;

public record EventDescriptionUpdated(int Id,
    string Description, 
    DateTime UpdatedAt);