using MediatR;

namespace EventSourcingConcepts.CQRS.Commands.RegisterThing;

public sealed record RegisterThingCommand(
    string ContainerId,
    string ExternalId,
    string Description,
    int Type) : IRequest<int>;