using AutoMapper;
using FluentMigrator.Runner;
using Scrapper.API.Auomapper;
using Scrapper.API.Endpoints.EndPointConfig;
using Scrapper.API.Migration;
using Scrapper.Data;
using Scrapper.Data.DataSetup;
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

//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();

app.Run();

//internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}


