using EventSourcingConcepts.Domain.Abstraction;
using EventSourcingConcepts.Domain.Thing.ThingEvents;
using EventSourcingConcepts.Stores.Abstraction.Events;

namespace EventSourcingConcepts.Domain.Thing;

/// <summary>
/// Aggregate that will manage the update of a thing description
/// </summary>
public sealed class UpdateDescriptionThingAggregate : IAggregate<string>
{
    private readonly ICollection<IEvent> _uncommittedEvents = new List<IEvent>();
    private readonly int _id;
    private readonly string _description = "";
    private readonly ThingState _state;

    public UpdateDescriptionThingAggregate(IReadOnlyCollection<IEvent> stream, ThingProjection? thingProjection)
    {
        _id = thingProjection?.Id ?? stream.First().StreamId;
        if(thingProjection != null)
        {
          _description = thingProjection.Description;
          _state = thingProjection.State;
        }
        else
        {
            _state = ThingState.Active;
        }
    
        foreach (var @event in stream)
        {
            switch (@event)
            {
                case ThingRegistered thingRegistered:
                    _description = thingRegistered.Description;
                    break;
                case ThingDescriptionUpdated thingDescriptionUpdated:
                    _description = thingDescriptionUpdated.Description;
                    break;
                case ThingDeleted:
                    _state = ThingState.Deleted;
                    break;
            }
        }
    }

    public bool CanExecute(string newDescription)
    {
        return _description != newDescription && _state == ThingState.Active;
    }

    public void Execute(string newDescription)
    {
        var thingDescriptionUpdated = new ThingDescriptionUpdated(_id, newDescription, DateTime.UtcNow);
        _uncommittedEvents.Add(thingDescriptionUpdated);
    }

    public IReadOnlyCollection<IEvent> GetUncommittedEvents()
    {
        return (IReadOnlyCollection<IEvent>)_uncommittedEvents;
    }
}