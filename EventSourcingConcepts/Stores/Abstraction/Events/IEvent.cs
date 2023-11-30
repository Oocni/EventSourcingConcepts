namespace EventSourcingConcepts.Stores.Abstraction.Events;

public interface IEvent
{
    int EventOrder { get; }
    int StreamId { get; }
    DateTime At { get; }
}