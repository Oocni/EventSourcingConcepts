using EventSourcingConcepts.Stores.Abstraction.Events;
using EventSourcingConcepts.Stores.EventsStore;
using MediatR;

namespace EventSourcingConcepts.CQRS.Queries.GetThingEvents;

public class GetThingEventsHandler : IRequestHandler<GetThingEventsQuery, IReadOnlyCollection<IEvent>>
{
    private readonly IEventsStore _eventsStore;

    public GetThingEventsHandler(IEventsStore eventsStore)
    {
        _eventsStore = eventsStore;
    }
    
    public Task<IReadOnlyCollection<IEvent>> Handle(GetThingEventsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult((IReadOnlyCollection<IEvent>)_eventsStore.LoadEventStream(request.ThingId));
    }
}