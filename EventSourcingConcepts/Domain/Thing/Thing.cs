namespace EventSourcingConcepts.Domain.Thing;

public sealed class Thing
{
    public int Id { get; set; }
    public string ContainerId { get; set; } = "";
    public string ExternalId { get; set; } = "";
    public string Description { get; set; } = "";
    public ThingType Type { get; set; }
    public ThingState State { get; set; }
}