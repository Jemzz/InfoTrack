using Scraper.Services.Dtos;
using Scraper.Services.Requests;

namespace Scraper.Services.Services
{
    public interface IRankingSearchService
    {
        Task<GetResponseDto<SearchEngineDto>> GetSearchEngineById(Guid searchEngineId);
        Task<GetResponseDto<List<SearchEngineDto>>> GetSearchEngines();
        Task<GetResponseDto<SearchRankingDto>> GetSearchEngineRankings(GetSearchRankingRequest request);
    }
}
