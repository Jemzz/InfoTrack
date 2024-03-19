using AutoMapper;
using Scraper.Data.Entities;
using Scraper.Services.Dtos;

namespace Scraper.Tests.AutoMapper
{
    public static class RankingMapperConfig
    {

        public static IMapper Configure()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SearchHistory, SearchHistoryDto>().ReverseMap();
                cfg.CreateMap<SearchEngines, SearchEngineDto>().ReverseMap();
            });

            return mapperConfiguration.CreateMapper();
        }
    }
}
