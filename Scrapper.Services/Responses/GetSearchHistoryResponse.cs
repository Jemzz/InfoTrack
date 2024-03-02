using Scrapper.Services.Dtos;

namespace Scrapper.Services.Responses
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
