using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Scraper.API.Models;
using Scraper.API.Modules.ModuleConfig;
using Scraper.Data.Implementations;
using Scraper.Data.Interfaces;
using Scraper.Services.Implementations;
using Scraper.Services.Requests;
using Scraper.Services.Services;

namespace Scraper.API.Modules
{
    public class SearchEngineEndpoint : IEndpoint
    {

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endPoints)
        {
            endPoints.MapPost("/rankings", async ([FromBody] RankingRequestModel request, [FromServices] IRankingSearchService _search, IRankingSearchHistoryService _searchHistory) =>
            {
                var rankings = await _search.GetSearchEngineRankings(new GetSearchRankingRequest
                {
                    Id = Guid.Parse(request.SearchId),
                    SearchText = request.SearchText,
                    PageSize = request.PageSize,
                });

                if (rankings.Error == null)
                {
                    var historyResp = await _searchHistory.CreateSearchHistory(new StoreSearchHistoryRequest
                    {
                        SearchText = request.SearchText,
                        URL = rankings.Data.SearchUrl,
                        Rankings = rankings.Data.Rankings,
                        SearchEngineId = Guid.Parse(request.SearchId)
                    });

                    if (historyResp.Error != null)
                    {
                        return Results.BadRequest(historyResp);
                    }

                    return Results.Ok(rankings);
                }

                return Results.BadRequest(rankings);
            })
            .WithName("Rankings")
            .WithOpenApi()
            .WithSummary("Stores url rankings");

            endPoints.MapGet("/searchengines", async ([FromServices] IRankingSearchService _search) =>
            {
                var rankings = await _search.GetSearchEngines();

                if (rankings.Error != null)
                {
                    return Results.BadRequest(rankings);
                }

                return Results.Ok(rankings);
            })
            .WithName("SearchEngines")
            .WithOpenApi()
            .WithSummary("Retrieves search engines");

            endPoints.MapGet("/searchenginesbyid", async (Guid id, [FromServices] IRankingSearchService _search) =>
            {
                var rankings = await _search.GetSearchEngineById(id);

                if (rankings.Error != null)
                {
                    return Results.BadRequest(rankings);
                }

                return Results.Ok(rankings);
            })
            .WithName("SearchEnginesById")
            .WithOpenApi()
            .WithSummary("Retrieves search engines by id");

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
