using ForgeMap;
using SampleLibrary.Domain.Commands.Author;
using SampleLibrary.Domain.Commands.Book;
using SampleLibrary.Domain.Commands.Publisher;
using SampleLibrary.Domain.Entities.ValueObjects;
using SampleLibrary.Domain.Events;

namespace SampleLibrary.Application.Mapping
{
    [ForgeMap]
    public partial class AppForger
    {
        public partial Domain.Entities.Author Forge(CreateAuthorCommand source);
        public partial Domain.Entities.Author Forge(UpdateAuthorCommand source);

        public partial Domain.Entities.Publisher Forge(CreatePublisherCommand source);
        public partial Domain.Entities.Publisher Forge(UpdatePublisherCommand source);

        public partial Publication Forge(PublicationCommand source);
        public partial Domain.Entities.Book Forge(CreateBookCommand source);
        public partial Domain.Entities.Book Forge(UpdateBookCommand source);

        public partial AuthorEvent Forge(Domain.Entities.Author source);
        public partial PublisherEvent Forge(Domain.Entities.Publisher source);
        public partial BookEvent Forge(Domain.Entities.Book source);
    }
}
