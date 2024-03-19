namespace Scraper.Services.SearchEngines
{
    public abstract class SearchEngineBase(IHttpClientFactory httpClient)
    {
        private readonly IHttpClientFactory _httpClient = httpClient;

        internal virtual HttpClient HandleCustomClientHeader()
        {
            var client = _httpClient.CreateClient();

            return client;
        }
    }
}
