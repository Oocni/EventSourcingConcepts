namespace EventSourcingConcepts.Domain.Common.Events;

public interface IEvent
{
    int EventOrder { get; }
    int StreamId { get; }
    DateTime At { get; }
}