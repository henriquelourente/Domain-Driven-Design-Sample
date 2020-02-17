using System;
using SampleLibrary.Core.Commands;

namespace SampleLibrary.Domain.Commands.Book
{
    public class DeleteBookCommand : CommandBase
    {
        public Guid Id { get; set; }
    }
}