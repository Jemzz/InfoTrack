namespace Scraper.Services.Dtos
{
    public class CreatedSearchHistoryDto
    {
        public required string SearchText { get; set; }
        public required string SearchEngineName { get; set; }
        public required string URL { get; set; }
        public required string Rankings { get; set; }
    }
}
