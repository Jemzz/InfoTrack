namespace Scraper.Services.Dtos
{
    public class SearchRankingDto
    {
        public SearchRankingDto()
        {
            Rankings = [];
        }

        public string SearchText { get; set; }
        public List<int> Rankings { get; set; }
    }
}
