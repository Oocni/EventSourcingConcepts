using EventSourcingConcepts.Domain.Common.Events;
using EventSourcingConcepts.Domain.Common.Projections;
using EventSourcingConcepts.Domain.Thing.ThingEvents;

namespace EventSourcingConcepts.Domain.Thing;

public sealed class ThingProjection : IProjection
{
    public int Id { get; set; }
    public string ContainerId { get; set; } = "";
    public string ExternalId { get; set; } = "";
    public string Description { get; set; } = "";
    public ThingType Type { get; set; }
    public ThingState State { get; set; }
    
    public static ThingProjection CreateThing(IEnumerable<IEvent> stream)
    {
        var thingProjection = new ThingProjection();
    
        foreach (var @event in stream)
        {
            switch (@event)
            {
                case ThingRegistered thingRegistered:
                    thingProjection.Id = thingRegistered.StreamId;
                    thingProjection.ContainerId = thingRegistered.ContainerId;
                    thingProjection.ExternalId = thingRegistered.ExternalId;
                    thingProjection.Description = thingRegistered.Description;
                    thingProjection.Type = thingRegistered.Type;
                    break;
                case ThingDescriptionUpdated thingDescriptionUpdated:
                    thingProjection.Description = thingDescriptionUpdated.Description;
                    break;
                case ThingDeleted:
                    thingProjection.State = ThingState.Deleted;
                    break;
            }
        }

        return thingProjection;
    }
}