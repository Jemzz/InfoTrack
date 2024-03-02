using Scrapper.API.Modules.ModuleConfig;

namespace Scrapper.API.Endpoints.EndPointConfig
{
    public static class EndPointsAbstraction
    {
        static readonly List<IEndpoint> registeredModules = [];

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            var modules = DiscoverModules();
            foreach (var module in modules)
            {
                module.RegisterModule(services);
                registeredModules.Add(module);
            }

            return services;
        }

        public static WebApplication RegisterEndpoints(this WebApplication app)
        {
            foreach (var module in registeredModules)
            {
                module.MapEndpoints(app);
            }
            return app;
        }

        private static IEnumerable<IEndpoint> DiscoverModules()
        {
            return typeof(IEndpoint).Assembly
            .GetTypes()
                .Where(p => p.IsClass && p.IsAssignableTo(typeof(IEndpoint)))
                .Select(Activator.CreateInstance)
                .Cast<IEndpoint>();
        }
    }
}
