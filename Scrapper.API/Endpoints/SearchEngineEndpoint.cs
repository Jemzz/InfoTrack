﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Scrapper.API.Modules.ModuleConfig;
using Scrapper.Data.Implementations;
using Scrapper.Data.Interfaces;
using Scrapper.Services.Implementations;
using Scrapper.Services.Services;

namespace Scrapper.API.Modules
{
    public class SearchEngineEndpoint : IEndpoint
    {

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endPoints)
        {
            endPoints.MapPost("/rankings", async (Guid searchId, string searchText, int pageSize, [FromServices] IRankingSearchService _search) =>
            {
                var rankings = await _search.GetSearchEngineRankings(new Services.Requests.GetSearchRankingRequest
                {
                    Id = searchId,
                    SearchText = searchText,
                    PageSize = pageSize,
                });

                return rankings;
            })
            .WithName("Rankings")
            .WithOpenApi();

            endPoints.MapGet("/serchengines", async ([FromServices] IRankingSearchService _search) =>
            {
                var rankings = await _search.GetSearchEngines();

                return rankings;
            })
            .WithName("SearchEngines")
            .WithOpenApi();

            endPoints.MapGet("/serchenginesbyid", async (Guid id, [FromServices] IRankingSearchService _search) =>
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
