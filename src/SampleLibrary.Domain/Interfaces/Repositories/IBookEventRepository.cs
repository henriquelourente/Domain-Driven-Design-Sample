using System;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Events;

namespace SampleLibrary.Domain.Interfaces.Repositories
{
    public interface IBookEventRepository : IElasticsearchRepository<BookEvent, Guid>
    {
    }
}
