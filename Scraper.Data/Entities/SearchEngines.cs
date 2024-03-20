namespace Scraper.Data.Entities
{
    public class SearchEngines
    {
        public required Guid? Id { get; set; }

        public required string SearchEngineName { get; set; }

        public required string Url { get; set; }
        public required string Regex { get; set; }
    }
}
