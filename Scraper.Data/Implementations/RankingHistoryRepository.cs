﻿using Dapper;
using Microsoft.Extensions.Options;
using Scraper.Data.Entities;
using Scraper.Data.Interfaces;

namespace Scraper.Data.Implementations
{
    // Used Dapper as opposed to EF for ease of use
    public class RankingHistoryRepository(IOptions<RepositoryOptions> configuration) : RepositoryBase(configuration), IRankingHistoryRepository
    {
        public async Task<IEnumerable<SearchHistory>> ReadSearchHistory()
        {
            using var cn = Connection;
            const string sql = "SELECT sh.Id, se.Id SearchEngineId, SearchText, sh.Url, SearchDate, Rankings, se.SearchEngineName FROM SearchHistory sh inner join SearchEngines se on sh.SearchEngineId = se.id";

            return await cn.QueryAsync<SearchHistory>(sql);
        }

        public async Task<SearchHistory> CreateSearch(CreateSearch search)
        {
            using var cn = Connection;
            const string sql = "INSERT INTO SearchHistory (Id, SearchText, Url, SearchDate, Rankings, SearchEngineId) VALUES (@Id, @SearchText, @Url, @SearchDate, @Rankings, @SearchEngineId)";

            var Id = Guid.NewGuid();

            var searchRecord = new
            {
                Id,
                search.SearchText,
                search.Url,
                SearchDate = DateTime.Now,
                search.Rankings,
                search.SearchEngineId
            };

            await cn.ExecuteAsync(sql, searchRecord);

            var insertedUser = await cn.QueryFirstOrDefaultAsync<SearchHistory>("SELECT sh.Id, se.Id SearchEngineId, SearchText, sh.Url, SearchDate, Rankings, se.SearchEngineName FROM SearchHistory sh inner join SearchEngines se on sh.SearchEngineId = se.id where sh.Id = @Id", new { Id });

            return insertedUser!;
        }
    }
}
