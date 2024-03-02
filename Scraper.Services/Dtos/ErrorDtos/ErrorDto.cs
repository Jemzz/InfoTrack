namespace Scraper.Services.Dtos.ErrorDtos
{
    public class ErrorDto
    {
        public ErrorDto()
        {
            Errors = [];
        }

        public string Message { get; set; }
        public int Code { get; set; }

        public IEnumerable<ErrorDetailDto> Errors { get; set; }
    }
}
