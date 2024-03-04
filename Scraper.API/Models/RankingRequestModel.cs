namespace Scraper.API.Models
{
    public class RankingRequestModel
    {
        public string SearchId { get; set; }
        public string SearchText { get; set; }
        public int PageSize { get; set; }
    }
}
