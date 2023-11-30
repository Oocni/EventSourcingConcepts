using EventSourcingConcepts.Domain.Thing.ThingEvents;
using EventSourcingConcepts.Stores.Abstraction.Events;
using EventSourcingConcepts.Stores.Abstraction.Projections;

namespace EventSourcingConcepts.Domain.Thing;

public sealed class ThingProjection : IProjection
{
    public int Id { get; set; }
    public string ContainerId { get; set; } = "";
    public string ExternalId { get; set; } = "";
    public string Description { get; set; } = "";
    public ThingType Type { get; set; }
    public ThingState State { get; set; }
    
    public ThingProjection(IEnumerable<IEvent> stream, IProjection? projection)
    {
        if (projection is ThingProjection thingProjection)
        {
            Id = projection.Id;
            ContainerId = thingProjection.ContainerId;
            ExternalId = thingProjection.ExternalId;
            Description = thingProjection.Description;
            Type = thingProjection.Type;
            State = thingProjection.State;
        }
    
        foreach (var @event in stream)
        {
            switch (@event)
            {
                case ThingRegistered thingRegistered:
                    Id = thingRegistered.StreamId;
                    ContainerId = thingRegistered.ContainerId;
                    ExternalId = thingRegistered.ExternalId;
                    Description = thingRegistered.Description;
                    Type = thingRegistered.Type;
                    break;
                case ThingDescriptionUpdated thingDescriptionUpdated:
                    Description = thingDescriptionUpdated.Description;
                    break;
                case ThingDeleted:
                    State = ThingState.Deleted;
                    break;
            }
        }
    }
}