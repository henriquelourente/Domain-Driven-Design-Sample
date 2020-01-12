using System;

namespace SampleLibrary.Domain.Commands.Author
{
    public class UpdateAuthorCommand : AuthorBaseCommand
    {
        public Guid Id { get; set; }
    }
}