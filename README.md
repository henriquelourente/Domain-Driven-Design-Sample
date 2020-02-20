# SampleLibrary
Example of how to build applications with ASP.NET Core and DDD concepts.

## How to use:

- You will need the latest Visual Studio 2019 and the latest .NET Core SDK.
- The latest SDK and tools can be downloaded from https://dot.net/core.
- You will need RabbitMQ message broker. 
- You will need a Elasticsearch database.

**Enviroment**

- [RabbitMQ](https://hub.docker.com/_/rabbitmq)
  - By default, application connect to http://localhost
- [Elasticsearch](https://www.elastic.co/guide/en/elasticsearch/reference/current/docker.html)
  - Address configured on appsettings.json

## Technologies implemented:

- ASP.NET Core 3.1
- Entity Framework Core 3.1
- .NET Core Native DI
- Entity Framework Core (with SQL Server)
- RabbitMQ ([EasyNetQ](https://easynetq.com/))
- Elasticsearch ([Nest](https://www.elastic.co/guide/en/elasticsearch/client/net-api/7.x/index.html))
- XUnit
- AutoMapper
- FluentValidation 

## Architecture:

- Responsibility separation concerns, SOLID, YAGNI and Clean Code
- Domain Driven Design (Layers and Domain Model Pattern)
- Domain Notification
- CQRS (Imediate Consistency)
- Repository
- IoC
- Messaging
