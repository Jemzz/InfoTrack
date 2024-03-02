namespace Scrapper.Services.Requests
{
    public class StoreSearchHistoryRequest
    {
        public required Guid SearchEngineId { get; set; }
        public required string SearchText { get; set; }
        public required string SearchEngineName { get; set; }
        public required string URL { get; set; }
        public required string Rankings { get; set; }
    }
}
