using Scrapper.Services.Dtos;
using Scrapper.Services.Requests;

namespace Scrapper.Services.Services
{
    public interface IRankingSearchHistoryService
    {
        Task<GetResponseDto<List<SearchHistoryDto>>> GetSearchHistory(GetSearchHistoryRequest request);
        Task<GetResponseDto<CreatedSearchHistoryDto>> CreateSearchHistory(StoreSearchHistoryRequest request);
    }
}
