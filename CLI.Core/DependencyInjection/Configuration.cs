using CLI.Entity.MappingProfiles;
using CLI.ExternalServices.Clients;
using CLI.Interfaces.ApiClient;
using CLI.Interfaces.Services;
using CLI.Services.Core;
using CLI.Services.Customer;
using Microsoft.Extensions.DependencyInjection;

namespace CLI.Core.DependencyInjection
{
    /// <summary>
    /// Responsible for configuring the dependency injection for the application
    /// </summary>
    public static class Configuration
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services, params string[] args)
        {
            //If I had more time, I would have implemented this using reflection to make this process automatic 

            services.AddHostedService<StarterService>();
            services.AddSingleton<ICustomerApiClient, CustomerApiClient>();
            services.AddSingleton<IGenericApiClient, GenericApiClient>();
            services.AddSingleton<ICustomerService, CustomerService>();

            services.AddAutoMapper((config) =>
            {
                config.AllowNullCollections = true;
                config.AllowNullDestinationValues = true;

            }, typeof(DtoToModelMappingProfile).Assembly);
        }
    }
}
