using MediatR;

namespace EventSourcingConcepts.CQRS.Commands.UpdateDescriptionThing;

public sealed record UpdateDescriptionThingCommand(int ThingId, string NewDescription) : IRequest;