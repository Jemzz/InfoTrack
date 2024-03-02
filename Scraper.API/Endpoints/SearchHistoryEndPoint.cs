using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Scraper.API.Modules.ModuleConfig;
using Scraper.Data.Implementations;
using Scraper.Data.Interfaces;
using Scraper.Services.Implementations;
using Scraper.Services.Services;

namespace Scraper.API.Endpoints
{
    public class SearchHistoryEndPoint : IEndpoint
    {
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endPoints)
        {
            endPoints.MapGet("/searchHistory", async (Guid? searchId, string searchText, string ranking, DateTime? searchDate, [FromServices] IRankingSearchHistoryService _searchHistory) =>
            {
                var searchHitory = await _searchHistory.GetSearchHistory(new Services.Requests.GetSearchHistoryRequest
                {
                    Id = searchId,
                    KeyWords = searchText,
                    Ranking = ranking,
                    SearchDate = searchDate
                });

                return searchHitory;
            })
            .WithName("History")
            .WithOpenApi();

            return endPoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            services.TryAddSingleton<IRankingHistoryRepository, RankingHistoryRepository>();
            services.TryAddSingleton<IRankingSearchHistoryService, RankingSearchHistoryService>();

            return services;
        }
    }
}
