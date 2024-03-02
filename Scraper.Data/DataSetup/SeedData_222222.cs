using FluentMigrator;
using Scraper.Data.Entities;

namespace Scraper.Data.DataSetup
{
    [Migration(222222)]
    public class SeedData_222222 : Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            Insert.IntoTable("SearchEngines")
            .Row(new SearchEngines
            {
                Id = Guid.NewGuid(),
                Url = "https://www.google.co.uk/search?num=(amount)&q=(searchToFind)",
                SearchEngineName = "Google",
                Regex = @"gMi0 kCrYT(.+?)sa=U&ved="
            });
            Insert.IntoTable("SearchEngines")
            .Row(new SearchEngines
            {
                Id = Guid.NewGuid(),
                Url = "https://www.bing.com/search?count=(amount)&q=(searchToFind)",
                SearchEngineName = "Bing",
                Regex = @"<h2>*?<a\s+[^>]*?href=\""([^\""]*)"
            });
        }
    }
}
