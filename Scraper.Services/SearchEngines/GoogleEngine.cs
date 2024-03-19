using System.Net;

namespace Scraper.Services.SearchEngines
{
    public class GoogleEngine(IHttpClientFactory httpClient) : SearchEngineBase(httpClient)
    {
        private readonly Uri BaseUrl = new("https://www.google.co.uk");

        internal override HttpClient HandleCustomClientHeader()
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(BaseUrl, new Cookie("CONSENT", "PENDING+986"));
            using var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
            var client = new HttpClient(handler)
            {
                BaseAddress = BaseUrl
            };

            return client;
        }
    }
}
