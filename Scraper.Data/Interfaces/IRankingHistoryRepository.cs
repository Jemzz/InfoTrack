using Scraper.Data.Entities;

namespace Scraper.Data.Interfaces
{
    public interface IRankingHistoryRepository : IRepository<SearchHistory>
    {
        Task<SearchHistory> CreateSearch(CreateSearch search);
    }
}
