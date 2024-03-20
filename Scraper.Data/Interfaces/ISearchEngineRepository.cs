using Scraper.Data.Entities;

namespace Scraper.Data.Interfaces
{
    public interface ISearchEngineRepository : IRepository<SearchEngines>
    {
        Task<SearchEngines> ReadSearchById(Guid Id);
    }
}
