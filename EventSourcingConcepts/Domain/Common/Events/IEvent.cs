namespace EventSourcingConcepts.Domain.Common.Events;

public interface IEvent
{
    int EventOrder { get; }
    int Id { get; }
    DateTime At { get; }
}