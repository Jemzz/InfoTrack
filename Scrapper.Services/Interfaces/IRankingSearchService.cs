using Scrapper.Services.Dtos;
using Scrapper.Services.Requests;

namespace Scrapper.Services.Services
{
    public interface IRankingSearchService
    {
        Task<GetResponseDto<SearchEngineDto>> GetSearchEngineById(Guid searchEngineId);
        Task<GetResponseDto<List<SearchEngineDto>>> GetSearchEngines();
        Task<GetResponseDto<SearchRankingDto>> GetSearchEngineRankings(GetSearchRankingRequest request);
    }
}
