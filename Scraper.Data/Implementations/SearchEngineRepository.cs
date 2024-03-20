using Dapper;
using Microsoft.Extensions.Options;
using Scraper.Data.Entities;
using Scraper.Data.Interfaces;
using System.Data;

namespace Scraper.Data.Implementations
{
    public class SearchEngineRepository(IOptions<RepositoryOptions> configuration) : RepositoryBase(configuration), ISearchEngineRepository
    {
        public async Task<IEnumerable<SearchEngines>> GetAll()
        {
            using var cn = Connection;
            const string sql = "SELECT * FROM SearchEngines";

            return await cn.QueryAsync<SearchEngines>(sql, CommandType.Text);
        }

        public async Task<SearchEngines> ReadSearchById(Guid Id)
        {
            using var cn = Connection;
            const string sql = "SELECT * FROM SearchEngines where Id = @Id";

            return await cn.QueryFirstAsync<SearchEngines>(sql, new { Id });
        }
    }
}
