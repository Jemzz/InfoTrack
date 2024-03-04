using AutoMapper;
using FluentMigrator.Runner;
using Scraper.API.Auomapper;
using Scraper.API.Endpoints.EndPointConfig;
using Scraper.API.Migration;
using Scraper.Data;
using Scraper.Data.DataSetup;
using SqlAlias;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.RegisterServices();
builder.Services.AddSingleton<Database>();
builder.Services.AddFluentMigratorCore().ConfigureRunner(c => c.AddSqlServer2016()
            .WithGlobalConnectionString(builder.Configuration.GetConnectionString("DefaultConnection"))
            .ScanIn(Assembly.GetAssembly(typeof(Database))).For.Migrations()); // .AddSingleton<IMigrationRunner>();

// Automapper configg
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new SearchProfile());
});

var mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Repository config from app settings
builder.Services.Configure<RepositoryOptions>(option =>
{
    option.ConnectionString = Aliases.Map(builder.Configuration.GetConnectionString("DefaultConnection"));
    option.MasterConnectionString = Aliases.Map(builder.Configuration.GetConnectionString("MasterConnection"));
});


// cors set up
builder.Services.AddCors();

var app = builder.Build();

app.MigrateDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.RegisterEndpoints();

app.UseCors(builder => builder
       .AllowAnyHeader()
       .AllowAnyMethod()
       .AllowAnyOrigin()
    );

app.Run();

