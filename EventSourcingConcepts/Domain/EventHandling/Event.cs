namespace EventSourcingConcepts.Domain.EventHandling;

internal sealed class Event
{
    public int Id { get; set; }
    public string ContainerId { get; set; }
    public string ExternalId { get; set; }
    public string Description { get; set; }
    public EventType Type { get; set; }

    private Event(int id, string containerId, string externalId, string description, EventType type)
    {
        Id = id;
        ContainerId = containerId;
        ExternalId = externalId;
        Description = description;
        Type = type;
    }
}