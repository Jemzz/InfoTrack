using Scrapper.Data.Entities;

namespace Scrapper.Data.Interfaces
{
    public interface ISearchEngineRepository
    {
        Task<IEnumerable<SearchEngines>> ReadSearchEngines();
    }
}
