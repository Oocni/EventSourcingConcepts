using EventSourcingConcepts.Domain.Common.Events;
using MediatR;

namespace EventSourcingConcepts.CQRS.Queries.GetThingEvents;

public sealed record GetThingEventsQuery(int ThingId):IRequest<IReadOnlyCollection<IEvent>>;
