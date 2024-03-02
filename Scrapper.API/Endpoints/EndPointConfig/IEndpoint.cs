namespace Scrapper.API.Modules.ModuleConfig
{
    public interface IEndpoint
    {
        IServiceCollection RegisterModule(IServiceCollection services);
        IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endPoints);
    }
}
