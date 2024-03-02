using AutoMapper;
using Scrapper.Data.Entities;
using Scrapper.Services.Dtos;

namespace Scrapper.API.Auomapper
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
