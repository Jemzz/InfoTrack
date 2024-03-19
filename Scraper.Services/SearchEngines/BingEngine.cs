
namespace Scraper.Services.SearchEngines
{
    public class BingEngine(IHttpClientFactory httpClient) : SearchEngineBase(httpClient)
    {
        internal override HttpClient HandleCustomClientHeader()
        {
            var client = base.HandleCustomClientHeader();

            client.DefaultRequestHeaders.Add("User-Agent",
                       "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_14_0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36");

            return client;
        }
    }
}
