using Scraper.Data.Entities;

namespace Scraper.Tests.MockData
{
    public static class SearchEngineMock
    {
        public static List<SearchEngines> SearchEngines =
        [
            new()
            {
                Id = Guid.Parse("84008369-7A16-4E19-9D79-2BD9332D98C5"),
                Url = "https://www.bing.com/search?count=(amount)&q=(searchToFind)",
                SearchEngineName = "Bing",
                Regex = @"<h2>*?<a\s+[^>]*?href=\""([^\""]*)"
            },
            new()
            {
                Id = Guid.Parse("0FEFF982-E9AA-4403-B6B8-9A6FD5B01B6D"),
                Url = "https://www.google.co.uk/search?num=(amount)&q=(searchToFind)",
                SearchEngineName = "Google",
                Regex = @"gMi0 kCrYT(.+?)sa=U&ved="
            }
        ];
    }
}
