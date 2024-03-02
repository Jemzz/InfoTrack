﻿using Dapper;
using Microsoft.Extensions.Options;
using Scrapper.Data.Entities;
using Scrapper.Data.Interfaces;

namespace Scrapper.Data.Implementations
{
    // Used Dapper as opposed to EF for ease of use
    public class RankingHistoryRepository(IOptions<RepositoryOptions> configuration) : RepositoryBase(configuration), IRankingHistoryRepository
    {
        public async Task<IEnumerable<SearchHistory>> ReadSearchHistory()
        {
            using var cn = Connection;
            const string sql = "SELECT sh.Id, SearchText, se.Url, SearchDate, Rankings, se.SearchEngineName FROM SearchHistory sh inner join SearchEngines se on sh.id = se.id";

            return await cn.QueryAsync<SearchHistory>(sql);
        }

        public async Task CreateSearch(string searchText, string url, string rankings, Guid searchEngineId)
        {
            using var cn = Connection;
            const string sql = "INSERT INTO SearchHistory (Id, SearchText, Url, SearchDate, Rankings, SearchEngineId) VALUES (@Id, @SearchText, @Url, @SearchDate, @Rankings, @SearchEngineId)";

            var searchRecord = new
            {
                Id = Guid.NewGuid(),
                SearchText = searchText,
                URL = url,
                SearchDate = DateTime.Now,
                Rankings = rankings,
                SearchEngineId = searchEngineId
            };

            await cn.ExecuteAsync(sql, searchRecord);
        }
    }
}
