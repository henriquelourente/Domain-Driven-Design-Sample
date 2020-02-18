using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SampleLibrary.Core.Interfaces;
using SampleLibrary.Domain.Events;

namespace SampleLibrary.Domain.Interfaces.Repositories
{
    public interface IBookEventRepository : IElasticsearchRepository<BookEvent, Guid>
    {
        Task<IEnumerable<BookEvent>> GetByTextAsync(string text);
    }
}
