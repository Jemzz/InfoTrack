namespace Scrapper.Services.Dtos
{
    public class GetResponseDto<T> : BaseResponseDto where T : class
    {
        public T Data { get; set; }
    }
}
