using Scraper.Data.Entities;

namespace Scraper.Data.Interfaces
{
    public interface IRankingHistoryRepository
    {
        Task<IEnumerable<SearchHistory>> ReadSearchHistory();

        Task CreateSearch(string searchText, string url, string rankings, Guid searchEngineId);
    }
}
