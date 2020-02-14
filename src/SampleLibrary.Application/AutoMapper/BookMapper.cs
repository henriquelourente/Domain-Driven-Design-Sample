using AutoMapper;
using SampleLibrary.Domain.Commands.Book;
using SampleLibrary.Domain.Events;
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

        public static BookEvent EntityToEvent(Domain.Entities.Book entity)
        {
            var config = new MapperConfiguration(configure =>
            {
                configure.CreateMap<Domain.Entities.Author, AuthorEvent>();
                configure.CreateMap<Domain.Entities.Publisher, PublisherEvent>();
                configure.CreateMap<Domain.Entities.Book, BookEvent>();
            });

            var mapper = config.CreateMapper();
            return mapper.Map<BookEvent>(entity);
        }
    }
}