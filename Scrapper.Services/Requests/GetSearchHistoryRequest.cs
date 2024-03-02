namespace Scrapper.Services.Requests
{
    public class GetSearchHistoryRequest
    {
        public Guid? Id { get; set; }
        public string? KeyWords { get; set; }
        public string? Ranking { get; set; }
        public DateTime? SearchDate { get; set; }
        public DateTime? SearchEndDate { get; set; }
    }
}
