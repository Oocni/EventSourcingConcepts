﻿// See https://aka.ms/new-console-template for more information

using EventSourcingConcepts.CQRS.Commands.DeleteThing;
using EventSourcingConcepts.CQRS.Commands.RegisterThing;
using EventSourcingConcepts.CQRS.Commands.UpdateDescriptionThing;
using EventSourcingConcepts.CQRS.Queries.GetThing;
using EventSourcingConcepts.Domain.Thing;
using EventSourcingConcepts.Stores.EventsStore;
using EventSourcingConcepts.Stores.ProjectionsStore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

//Add dependency injection
var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<IEventsStore, EventsStore>();
builder.Services.AddSingleton<IProjectionsStore, ProjectionsStore>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<RegisterThingCommand>());

using var host = builder.Build();
using var serviceScope = host.Services.CreateScope();
var serviceProvider = serviceScope.ServiceProvider;
var mediator = serviceProvider.GetRequiredService<IMediator>();

Console.WriteLine("Event Sourcing Concepts");
Console.WriteLine("-----------------------");

var @continue = true;
while (@continue)
{
    Console.WriteLine("- (r)egister a thing");
    Console.WriteLine("- (u)pdate a thing (d)escription");
    Console.WriteLine("- (d)elete a thing");
    Console.WriteLine("- (g)et a thing");
    Console.WriteLine("- (e)xit the program");
    var userInput = Console.ReadLine();

    switch (userInput)
    {
        case "r" : RegisterThing(); break;
        case "ud" : UpdateThingDescription(); break;
        case "d" : DeleteThing(); break;
        case "g" : GetThing(); break;
        case "e" : @continue = false; break;
        default : Console.WriteLine("Invalid input"); break;
    }
}

return;

void RegisterThing()
{
    Console.WriteLine("Enter the container id");
    var containerId = Console.ReadLine();
    Console.WriteLine("Enter the external id");
    var externalId = Console.ReadLine();
    Console.WriteLine("Enter the description");
    var description = Console.ReadLine();
    Console.WriteLine("Enter the event type");
    var eventType = (ThingType)Enum.Parse(typeof(ThingType), Console.ReadLine());

    var command = new RegisterThingCommand(containerId, externalId, description, (int)eventType);
    mediator.Send(command);
}

void UpdateThingDescription()
{
    Console.WriteLine("Enter the id");
    var id = int.Parse(Console.ReadLine());
    Console.WriteLine("Enter the new description");
    var description = Console.ReadLine();

    var command = new UpdateDescriptionThingCommand(id, description);
    mediator.Send(command);
}

void DeleteThing()
{
    Console.WriteLine("Enter the id");
    var eventId = Console.ReadLine();

    var command = new DeleteThingCommand(int.Parse(eventId));
    mediator.Send(command);
}

void GetThing()
{
    Console.WriteLine("Enter the id");
    var id =int.Parse(Console.ReadLine());

    var query = new GetThingQuery(id);
    var thing = mediator.Send(query).Result;

    if(thing == null)
    {
        Console.WriteLine("Thing not found");
        return;
    }
    
    Console.WriteLine($"Thing id: {thing.Id}");
    Console.WriteLine($"Thing container id: {thing.ContainerId}");
    Console.WriteLine($"Thing external id: {thing.ExternalId}");
    Console.WriteLine($"Thing description: {thing.Description}");
    Console.WriteLine($"Thing type: {thing.Type}");
    Console.WriteLine($"Thing state: {thing.State}");
}