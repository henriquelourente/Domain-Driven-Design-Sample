# C# Domain-Driven Design Sample
Example of how to build applications with ASP.NET Core and DDD concepts.

## How to use:

- You will need Visual Studio and the current .NET SDK.
- The current SDK and tools can be downloaded from https://dot.net/core.
- You will need RabbitMQ message broker. 
- You will need a Elasticsearch database.

**Enviroment**

- [RabbitMQ](https://hub.docker.com/_/rabbitmq)
  - By default, application connect to http://localhost:5672
- [Elasticsearch](https://www.elastic.co/guide/en/elasticsearch/reference/current/docker.html)
  - Address configured on appsettings.json

## Technologies implemented:

- ASP.NET
- .NET Core Native DI
- Entity Framework Core (with SQL Server)
- RabbitMQ ([EasyNetQ](https://easynetq.com/))
- Elasticsearch ([Nest](https://www.elastic.co/guide/en/elasticsearch/client/net-api/7.x/index.html))
- XUnit
- AutoMapper
- FluentValidation 
- Swagger

## Architecture:

- Responsibility separation concerns, SOLID, YAGNI and Clean Code
- Domain Driven Design (Layers and Domain Model Pattern)
- Domain Notification
- CQRS (Imediate Consistency)
- Repository
- IoC
- Messaging
