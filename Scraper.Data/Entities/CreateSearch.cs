namespace Scraper.Data.Entities
{
    public class CreateSearch
    {
        public required string SearchText { get; set; }
        public required string Url { get; set; }
        public required string Rankings { get; set; }
        public required Guid SearchEngineId { get; set; }
    }
}
