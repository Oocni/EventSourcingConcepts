using MediatR;

namespace EventSourcingConcepts.CQRS.Commands.DeleteThing;

public sealed record DeleteThingCommand(int ThingId) : IRequest;