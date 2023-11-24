using EventSourcingConcepts.Domain.Common.Events;
using EventSourcingConcepts.Domain.Thing.ThingEvents;

namespace EventSourcingConcepts.Domain.Thing;

public sealed class DeleteThingAggregate
{
    public ThingState State { get; set; }
    
    public static DeleteThingAggregate CreateDeleteThingAggregate(IEnumerable<IEvent> stream)
    {
        var deleteThingAggregate = new DeleteThingAggregate();
    
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