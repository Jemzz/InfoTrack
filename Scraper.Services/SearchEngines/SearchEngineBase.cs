namespace Scraper.Services.SearchEngines
{
    public abstract class SearchEngineBase(IHttpClientFactory httpClient)
    {
        private readonly IHttpClientFactory _httpClient = httpClient;

        // Used a class/virtual method as opposed to an interface because not every engine will have an implementation
        internal virtual HttpClient HandleCustomClientHeader()
        {
            var client = _httpClient.CreateClient();

            return client;
        }
    }
}
