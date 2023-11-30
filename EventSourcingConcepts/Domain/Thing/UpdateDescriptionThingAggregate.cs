using EventSourcingConcepts.Domain.Thing.ThingEvents;
using EventSourcingConcepts.Stores.Abstraction.Events;
using EventSourcingConcepts.Stores.EventsStore;

namespace EventSourcingConcepts.Domain.Thing;

public sealed class UpdateDescriptionThingAggregate
{
    public string Description { get; set; } = "";
    public ThingState State { get; set; }
    
    public static UpdateDescriptionThingAggregate CreateUpdateDescriptionThingAggregate(IEnumerable<IEvent> stream, ThingProjection? thingProjection)
    {
        var updateDescriptionThingAggregate = thingProjection != null 
            ? new UpdateDescriptionThingAggregate
                { Description = thingProjection.Description, State = thingProjection.State }
            : new UpdateDescriptionThingAggregate();
    
        foreach (var @event in stream)
        {
            switch (@event)
            {
                case ThingRegistered thingRegistered:
                    updateDescriptionThingAggregate.Description = thingRegistered.Description;
                    break;
                case ThingDescriptionUpdated thingDescriptionUpdated:
                    updateDescriptionThingAggregate.Description = thingDescriptionUpdated.Description;
                    break;
                case ThingDeleted:
                    updateDescriptionThingAggregate.State = ThingState.Deleted;
                    break;
            }
        }

        return updateDescriptionThingAggregate;
    }
}