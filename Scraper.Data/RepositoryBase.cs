using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace Scraper.Data
{
    public class RepositoryBase(IOptions<RepositoryOptions> configuration)
    {
        private readonly IOptions<RepositoryOptions> _configuration = configuration;

        protected string ConnectionString => _configuration.Value.ConnectionString;
        protected string MasterConnectionString => _configuration.Value.MasterConnectionString;

        protected IDbConnection Connection => new SqlConnection(ConnectionString);
        protected IDbConnection MasterConnection => new SqlConnection(MasterConnectionString);
    }
}
