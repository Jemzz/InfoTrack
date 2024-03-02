using Scrapper.Services.Dtos;

namespace Scrapper.Services.Responses
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
