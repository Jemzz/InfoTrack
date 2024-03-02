using Scraper.Services.Dtos;
using Scraper.Services.Requests;

namespace Scraper.Services.Services
{
    public interface IRankingSearchHistoryService
    {
        Task<GetResponseDto<List<SearchHistoryDto>>> GetSearchHistory(GetSearchHistoryRequest request);
        Task<GetResponseDto<CreatedSearchHistoryDto>> CreateSearchHistory(StoreSearchHistoryRequest request);
    }
}
