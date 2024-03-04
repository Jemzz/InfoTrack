using AutoMapper;
using Scraper.Data.Entities;
using Scraper.Services.Dtos;

namespace Scraper.API.Auomapper
{
    public class SearchProfile : Profile
    {
        public SearchProfile()
        {
            CreateMap<SearchHistory, SearchHistoryDto>().ReverseMap();
            CreateMap<SearchEngines, SearchEngineDto>().ReverseMap();
        }
    }
}
