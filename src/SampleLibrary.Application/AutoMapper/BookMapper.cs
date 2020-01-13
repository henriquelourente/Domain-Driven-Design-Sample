using AutoMapper;
using SampleLibrary.Domain.Commands.Author;
using SampleLibrary.Domain.Commands.Book;
using SampleLibrary.Domain.Tests.Entities.Validators.Entities.ValueObjects;

namespace SampleLibrary.Application.AutoMapper
{
    public static class BookMapper
    {
        public static Domain.Entities.Book CommandToEntity(CreateBookCommand command)
        {
            var config = new MapperConfiguration(configure =>
            {
                configure.CreateMap<CreateBookCommand, Domain.Entities.Book>();
                configure.CreateMap<PublicationCommand, Publication>();
            });

            var mapper = config.CreateMapper();
            return mapper.Map<Domain.Entities.Book>(command);
        }
        
        public static Domain.Entities.Book CommandToEntity(UpdateBookCommand command)
        {
            var config = new MapperConfiguration(configure =>
            {
                configure.CreateMap<UpdateBookCommand, Domain.Entities.Book>();
                configure.CreateMap<PublicationCommand, Publication>();
            });

            var mapper = config.CreateMapper();
            return mapper.Map<Domain.Entities.Book>(command);
        }
    }
}