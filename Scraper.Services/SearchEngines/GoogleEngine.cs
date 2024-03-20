namespace Scraper.Services.SearchEngines
{
    public class GoogleEngine(IHttpClientFactory httpClient) : SearchEngineBase(httpClient)
    {
        internal override HttpClient HandleCustomClientHeader()
        {
            return base.HandleCustomClientHeader();
        }
    }
}
