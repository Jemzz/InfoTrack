namespace Scrapper.Services.Dtos
{
    public class SearchEngineDto
    {
        public required Guid Id { get; set; }
        public required string SearchEngineName { get; set; }
        public required string Url { get; set; }
        public required string Regex { get; set; }
    }
}
