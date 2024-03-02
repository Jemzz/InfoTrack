namespace Scrapper.Data.Entities
{
    public class SearchHistory
    {
        public Guid Id { get; set; }
        public string SearchText { get; set; }
        public string Url { get; set; }
        public string Rankings { get; set; }
        public string SearchEngineName { get; set; }
        public DateTime SearchDate { get; set; }
    }
}
