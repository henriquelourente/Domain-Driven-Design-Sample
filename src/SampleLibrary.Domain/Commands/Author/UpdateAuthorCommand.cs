using System;

namespace SampleLibrary.Domain.Commands.Author
{
    public class UpdateAuthorCommand : AuthorCommandBase
    {
        public Guid Id { get; set; }
    }
}