using Scrapper.Data.Entities;

namespace Scrapper.Data.Interfaces
{
    public interface IRankingHistoryRepository
    {
        Task<IEnumerable<SearchHistory>> ReadSearchHistory();

        Task CreateSearch(string searchText, string url, string rankings, Guid searchEngineId);
    }
}
