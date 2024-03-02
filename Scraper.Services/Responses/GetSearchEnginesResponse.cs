using Scraper.Services.Dtos;

namespace Scraper.Services.Responses
{
    public class GetSearchEnginesResponse
    {
        public GetSearchEnginesResponse()
        {
            SearchEngines = [];
        }

        public List<SearchEngineDto> SearchEngines { get; set; }
    }
}
