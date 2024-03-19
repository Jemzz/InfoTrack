using Scraper.Data.Entities;

namespace Scraper.Tests.MockData
{
    public class SearchHistoryMock
    {
        public static List<SearchHistory> SearchHistory =
     [
         new()
         {
             Id = Guid.Parse("606F4B5A-52EA-4EF6-B9D6-80E7F122EAF1"),
             SearchText = "infotrack",
             Url = "https://www.bing.com/search?count=100&q=infotrack",
             Rankings = "1,2,3,5,6,8,9,10",
             SearchEngineId = Guid.Parse("84008369-7A16-4E19-9D79-2BD9332D98C5"),
             SearchEngineName = "Bing",
             SearchDate = DateTime.Now
         },
         new()
         {
             Id = Guid.Parse("1E0BBF8F-DCC0-4647-A838-9B02E6043CBD"),
             SearchText = "infotrack conveyancing",
             Url = "https://www.google.co.uk/search?num=100&q=infotrack+conveyancing",
             Rankings = "1",
             SearchEngineId = Guid.Parse("0FEFF982-E9AA-4403-B6B8-9A6FD5B01B6D"),
             SearchEngineName = "Google",
             SearchDate = DateTime.Now
         },
         new()
         {
             Id = Guid.Parse("78ECD15F-3F76-441C-BAFB-F7F0E8C426FB"),
             SearchText = "infotrack land conveyancing",
             Url = "https://www.google.co.uk/search?num=100&q=infotrack+conveyancing",
             Rankings = "1,4",
             SearchEngineId = Guid.Parse("0FEFF982-E9AA-4403-B6B8-9A6FD5B01B6D"),
             SearchEngineName = "Google",
             SearchDate = DateTime.Now
         }
     ];
    }
}
