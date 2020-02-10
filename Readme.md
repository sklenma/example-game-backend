# Example-Backend-For-Game

This example shows simple backend that can be used for game with levels and collecting rewards. 

##Before you start

Note that connection string isn't in appsettings.json, but directly in ExampleDbContext in method OnConfiguring, this is beacuse I was unable quickly resolve this problem with context pooling, that worked for MySQL and Ef core 2.2

Set startup project to Example-persistance and update database.
Set startup project Example-API and run.

## Architecture

There are 3 layers, API (Presentation layer) , Service(Business logic layer) and Persistance(Data layer). Application also use static definitions, that are predefined values for game. In this example there is definition of Awards for each level.

### Persistance

Contains entity classes,dbcontext and migrations

### Service

*Constants - Enums and Constant values used in backend
*Definitions - Model and logic for creating static definitions class for entire backend from json files.
*Exceptions - Custom exceptions for easier error handling
*Helpers - Logic for determining categories and other metadata
*Mappers - Mapping between layers and to client objects
*Resources - Contains definition jsons
*Value objects - Client objects, that is shared with unity client

### API

Contain controllers and configuration for running solution

