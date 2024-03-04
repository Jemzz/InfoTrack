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
            endPoints.MapGet("/searchHistory", async (string? searchId, string? searchText, [FromServices] IRankingSearchHistoryService _searchHistory) =>
            {
                var searchHitory = await _searchHistory.GetSearchHistory(new Services.Requests.GetSearchHistoryRequest
                {
                    Id = !string.IsNullOrEmpty(searchId) ? Guid.Parse(searchId) : null,
                    KeyWords = searchText
                });

                return searchHitory;
            })
            .WithName("History")
            .WithOpenApi()
            .WithSummary("Returns Search History");

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
