using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SampleLibrary.Application.Author;
using SampleLibrary.Application.Publisher;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Commands.Author;
using SampleLibrary.Domain.Commands.Author.Validators;
using SampleLibrary.Domain.Commands.Publisher;
using SampleLibrary.Domain.Commands.Publisher.Validators;
using SampleLibrary.Domain.Interfaces.Repositories;
using SampleLibrary.Infra.Data.Context;
using SampleLibrary.Infra.Data.Repositories;

namespace SampleLibrary.IoC
{
    public class SampleLibraryModule
    {
        public void Register(IServiceCollection services)
        {
            //data
            services.AddScoped<SampleLibraryContext>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IPublisherRepository, PublisherRepository>();

            //validators
            services.AddScoped<IValidator<CreateAuthorCommand>, CreateAuthorCommandValidator>();
            services.AddScoped<IValidator<UpdateAuthorCommand>, UpdateAuthorCommandValidator>();
            
            services.AddScoped<IValidator<CreatePublisherCommand>, CreatePublisherCommandValidator>();
            services.AddScoped<IValidator<UpdatePublisherCommand>, UpdatePublisherCommandValidator>();
            
            //commandHandlers
            services.AddScoped<ICommandHandler<CreateAuthorCommand>, CreateAuthorCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateAuthorCommand>, UpdateAuthorCommandHandler>();
            
            services.AddScoped<ICommandHandler<CreatePublisherCommand>, CreatePublisherCommandHandler>();
            services.AddScoped<ICommandHandler<UpdatePublisherCommand>, UpdatePublisherCommandHandler>();
            
            //queries
            services.AddScoped<IAuthorQueries, AuthorQueries>();
            services.AddScoped<IPublisherQueries, PublisherQueries>();
        }
    }
}