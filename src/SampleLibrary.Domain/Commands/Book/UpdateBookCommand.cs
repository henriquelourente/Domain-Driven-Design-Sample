using System;

namespace SampleLibrary.Domain.Commands.Book
{
    public class UpdateBookCommand : BookCommandBase
    {
        public Guid Id { get; set; }
    }
}