using Dapper;
using Microsoft.Extensions.Options;
using Scrapper.Data.Entities;
using Scrapper.Data.Interfaces;
using System.Data;

namespace Scrapper.Data.Implementations
{
    public class SearchEngineRepository(IOptions<RepositoryOptions> configuration) : RepositoryBase(configuration), ISearchEngineRepository
    {
        public async Task<IEnumerable<SearchEngines>> ReadSearchEngines()
        {
            using var cn = Connection;
            const string sql = "SELECT * FROM SearchEngines";

            return await cn.QueryAsync<SearchEngines>(sql, CommandType.Text);
        }
    }
}
