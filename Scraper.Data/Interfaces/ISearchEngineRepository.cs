using Scraper.Data.Entities;

namespace Scraper.Data.Interfaces
{
    public interface ISearchEngineRepository
    {
        Task<IEnumerable<SearchEngines>> ReadSearchEngines();
    }
}
