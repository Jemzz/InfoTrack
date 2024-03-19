namespace Scraper.Services.Requests
{
    public class StoreSearchHistoryRequest
    {
        public required Guid SearchEngineId { get; set; }
        public required string SearchText { get; set; }
        public required string URL { get; set; }
        public required List<int> Rankings { get; set; }
    }
}
