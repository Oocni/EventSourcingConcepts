using EventSourcingConcepts.Domain.Thing.ThingEvents;
using EventSourcingConcepts.Stores.Abstraction.Events;
using EventSourcingConcepts.Stores.EventsStore;

namespace EventSourcingConcepts.Domain.Thing;

public sealed class DeleteThingAggregate
{
    public ThingState State { get; set; }
    
    public static DeleteThingAggregate CreateDeleteThingAggregate(IEnumerable<IEvent> stream, ThingProjection? thingProjection)
    {
        var deleteThingAggregate = thingProjection != null
            ? new DeleteThingAggregate
                { State = thingProjection.State }
            : new DeleteThingAggregate();
    
        foreach (var @event in stream)
        {
            switch (@event)
            {
                case ThingDeleted:
                    deleteThingAggregate.State = ThingState.Deleted;
                    break;
            }
        }

        return deleteThingAggregate;
    }
}