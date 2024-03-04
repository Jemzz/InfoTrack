using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Scraper.API.Models;
using Scraper.API.Modules.ModuleConfig;
using Scraper.Data.Implementations;
using Scraper.Data.Interfaces;
using Scraper.Services.Implementations;
using Scraper.Services.Services;

namespace Scraper.API.Modules
{
    public class SearchEngineEndpoint : IEndpoint
    {

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endPoints)
        {
            endPoints.MapPost("/rankings", async ([FromBody] RankingRequestModel request, [FromServices] IRankingSearchService _search) =>
            {
                var rankings = await _search.GetSearchEngineRankings(new Services.Requests.GetSearchRankingRequest
                {
                    Id = Guid.Parse(request.SearchId),
                    SearchText = request.SearchText,
                    PageSize = request.PageSize,
                });

                return rankings;
            })
            .WithName("Rankings")
            .WithOpenApi();

            endPoints.MapGet("/searchengines", async ([FromServices] IRankingSearchService _search) =>
            {
                var rankings = await _search.GetSearchEngines();

                return rankings;
            })
            .WithName("SearchEngines")
            .WithOpenApi();

            endPoints.MapGet("/searchenginesbyid", async (Guid id, [FromServices] IRankingSearchService _search) =>
            {
                var rankings = await _search.GetSearchEngineById(id);

                return rankings;
            })
            .WithName("SearchEnginesById")
            .WithOpenApi();

            return endPoints;
        }

        public IServiceCollection RegisterModule(IServiceCollection services)
        {
            services.TryAddSingleton<IRankingSearchService, RankingSearchService>();
            services.TryAddSingleton<ISearchEngineRepository, SearchEngineRepository>();
            return services;
        }
    }
}
