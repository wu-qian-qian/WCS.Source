using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Common.Extensions.MediatR
{
    public static class MediatRExtensions
    {
        public static IServiceCollection AddMediatRConfigurator(this IServiceCollection services, Assembly[] moduleAssemblies)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(moduleAssemblies);
            });
            return services;
        }
    }
}
