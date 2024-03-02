using Scraper.Services.Dtos;

namespace Scraper.Services.Responses
{
    public class GetSearchHistoryResponse
    {
        public GetSearchHistoryResponse()
        {
            SearchHistory = [];
        }

        public List<SearchHistoryDto> SearchHistory { get; set; }
    }
}
