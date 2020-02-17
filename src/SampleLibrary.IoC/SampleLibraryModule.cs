using System;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SampleLibrary.Application.Author;
using SampleLibrary.Application.Book;
using SampleLibrary.Application.Publisher;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Commands.Author;
using SampleLibrary.Domain.Commands.Author.Validators;
using SampleLibrary.Domain.Commands.Book;
using SampleLibrary.Domain.Commands.Book.Validators;
using SampleLibrary.Domain.Commands.Publisher;
using SampleLibrary.Domain.Commands.Publisher.Validators;
using SampleLibrary.Domain.Events;
using SampleLibrary.Domain.Interfaces.Repositories;
using SampleLibrary.Infra.Data.Context;
using SampleLibrary.Infra.Data.Elasticsearch;
using SampleLibrary.Infra.Data.Elasticsearch.Configuration;
using SampleLibrary.Infra.Data.Elasticsearch.Interfaces;
using SampleLibrary.Infra.Data.Repositories;
using SampleLibrary.Infra.Data.Repositories.Elasticsearch;
using SampleLibrary.Infra.Messaging;

namespace SampleLibrary.IoC
{
    public static class SampleLibraryModule
    {
        public static void Register(this IServiceCollection services)
        {
            //data
            services.AddTransient<SampleLibraryContext>();

            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IPublisherRepository, PublisherRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookEventRepository, BookEventRepository>();

            //validators
            services.AddScoped<IValidator<CreateAuthorCommand>, CreateAuthorCommandValidator>();
            services.AddScoped<IValidator<UpdateAuthorCommand>, UpdateAuthorCommandValidator>();

            services.AddScoped<IValidator<CreatePublisherCommand>, CreatePublisherCommandValidator>();
            services.AddScoped<IValidator<UpdatePublisherCommand>, UpdatePublisherCommandValidator>();

            services.AddScoped<IValidator<CreateBookCommand>, CreateBookCommandValidator>();
            services.AddScoped<IValidator<UpdateBookCommand>, UpdateBookCommandValidator>();
            services.AddScoped<IValidator<DeleteBookCommand>, DeleteBookCommandValidator>();
            services.AddScoped<IValidator<PublicationCommand>, PublicationCommandValidator>();

            //commandHandlers
            services.AddScoped<ICommandHandler<CreateAuthorCommand>, CreateAuthorCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateAuthorCommand>, UpdateAuthorCommandHandler>();

            services.AddScoped<ICommandHandler<CreatePublisherCommand>, CreatePublisherCommandHandler>();
            services.AddScoped<ICommandHandler<UpdatePublisherCommand>, UpdatePublisherCommandHandler>();

            services.AddScoped<ICommandHandler<CreateBookCommand>, CreateBookCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateBookCommand>, UpdateBookCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteBookCommand>, DeleteBookCommandHandler>();

            //queries
            services.AddScoped<IAuthorQueries, AuthorQueries>();
            services.AddScoped<IPublisherQueries, PublisherQueries>();
            services.AddScoped<IBookQueries, BookQueries>();

            //messaging
            services.AddScoped<IEventPublisher<BookEvent>, EventPublisher<BookEvent>>();
            services.AddScoped<IEventPublisher<DeleteBookEvent>, EventPublisher<DeleteBookEvent>>();
            services.AddTransient<IEventConsumer<BookEvent, Guid>, BookEventConsumer>();
            services.AddTransient<IEventConsumer<DeleteBookEvent, Guid>, DeleteBookEventConsumer>();
            services.AddScoped<IConsumerSubscriptions, ConsumerSubscriptions>();

            //elasticsearch
            services.AddTransient<IElasticContextProvider, ElasticContextProvider>();
            services.AddTransient<IElasticConfigurationService, ElasticConfigurationService>();
        }
    }
}