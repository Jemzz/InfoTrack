using FluentMigrator;

namespace Scrapper.Data.DataSetup
{
    [Migration(111111)]
    public class InitialTables_111111 : Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            if (Schema.Schema("dbo").Table("SearchHistory").Exists())
            {
                Delete.Table("SearchHistory");
            }

            if (Schema.Schema("dbo").Table("SearchEngines").Exists())
            {
                Delete.Table("SearchEngines");
            }

            Create.Table("SearchEngines")
            .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
            .WithColumn("SearchEngineName").AsString(50).NotNullable()
            .WithColumn("Url").AsString(100).NotNullable()
            .WithColumn("Regex").AsString(int.MaxValue).NotNullable();

            Create.Table("SearchHistory")
            .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
            .WithColumn("SearchText").AsString(100).NotNullable()
            .WithColumn("Url").AsString(int.MaxValue).NotNullable()
            .WithColumn("SearchDate").AsDateTime().NotNullable()
            .WithColumn("Rankings").AsString(100).Nullable()
            .WithColumn("SearchEngineId").AsGuid().NotNullable().ForeignKey("SearchEngines", "Id");
        }
    }
}
