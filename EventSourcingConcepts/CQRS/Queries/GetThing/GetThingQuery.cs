using EventSourcingConcepts.Domain.Thing;
using MediatR;

namespace EventSourcingConcepts.CQRS.Queries.GetThing;

public sealed record GetThingQuery(int ThingId):IRequest<Thing>;
