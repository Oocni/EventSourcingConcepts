using EventSourcingConcepts.Domain.Abstraction;
using EventSourcingConcepts.Domain.Thing.ThingEvents;
using EventSourcingConcepts.Stores.Abstraction.Events;

namespace EventSourcingConcepts.Domain.Thing;

/// <summary>
/// Aggregate that will manage the deletion of a thing
/// </summary>
public sealed class DeleteThingAggregate : IAggregate
{
    private readonly ICollection<IEvent> _uncommittedEvents = new List<IEvent>();
    private readonly int _id;
    private readonly ThingState _state;

    public DeleteThingAggregate(IReadOnlyCollection<IEvent> stream, ThingProjection? thingProjection)
    {
        _id = thingProjection?.Id ?? stream.First().StreamId;
        _state = thingProjection != null
            ? _state = thingProjection.State
            : ThingState.Active;
    
        foreach (var @event in stream)
        {
            _state = @event switch
            {
                ThingDeleted => ThingState.Deleted,
                _ => _state
            };
        }
    }

    public bool CanExecute()
    {
        return _state == ThingState.Active;
    }

    public void Execute()
    {
        var thingDeleted = new ThingDeleted(_id, DateTime.UtcNow);
        _uncommittedEvents.Add(thingDeleted);
    }
    
    public IReadOnlyCollection<IEvent> GetUncommittedEvents()
    {
        return (IReadOnlyCollection<IEvent>)_uncommittedEvents;
    }
}