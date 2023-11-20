namespace EventSourcingConcepts.Domain.ThingHandling.ThingEvents;

public interface IEvent
{
    int EventOrder { get; }
    int Id { get; }
    DateTime At { get; }
}