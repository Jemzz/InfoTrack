using Dapper;
using Microsoft.Extensions.Options;

namespace Scraper.Data.DataSetup
{
    public class Database(IOptions<RepositoryOptions> configuration) : RepositoryBase(configuration)
    {
        public bool CreateDatabase(string dbName)
        {
            var tableAlreadyExists = true;

            var query = "SELECT * FROM sys.databases WHERE name = @name";
            using var cn = MasterConnection;
            var parameters = new DynamicParameters();
            parameters.Add("name", dbName);

            var records = cn.Query(query, parameters);
            if (!records.Any())
            {
                tableAlreadyExists = false;
                cn.Execute($"CREATE DATABASE {dbName}");
            }

            return tableAlreadyExists;
        }

    }
}
