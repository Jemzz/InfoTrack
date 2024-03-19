using Scraper.Data.Entities;

namespace Scraper.Data.Interfaces
{
    public interface IRankingHistoryRepository
    {
        Task<IEnumerable<SearchHistory>> ReadSearchHistory();

        Task<SearchHistory> CreateSearch(CreateSearch search);
    }
}
