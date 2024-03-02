using FluentMigrator.Runner;
using Scraper.Data.DataSetup;
namespace Scraper.API.Migration
{
    public static class MigrateExtension
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var databaseService = scope.ServiceProvider.GetRequiredService<Database>();
                var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                try
                {
                    databaseService.CreateDatabase("ScraperDB");

                    migrationService.MigrateUp(222222);
                    migrationService.MigrateUp(111111);
                }
                catch
                {
                    throw;
                }
            }

            return host;
        }
    }
}
