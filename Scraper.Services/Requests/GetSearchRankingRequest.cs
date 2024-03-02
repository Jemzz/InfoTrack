namespace Scraper.Services.Requests
{
    public class GetSearchRankingRequest
    {
        public Guid Id { get; set; }
        public string SearchText { get; set; }
        public int PageSize { get; set; }
    }
}
