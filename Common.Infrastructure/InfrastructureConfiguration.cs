using Common.Application.Encodings;
using Common.Domain;
using Common.Infrastructure.Authentication;
using Common.Infrastructure.Authorization;
using Common.Infrastructure.Event;
using Common.Infrastructure.Middleware;
using Common.Infrastructure.QuartzJobs;
using Common.Infrastructure.Swagger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, Assembly[] assemblies ,IConfiguration configuration)
        {
            // 配置异常拦截事件
            services.AddGlobalException();
            //配置SwaggerUI
            services.AddSwaggerConfigurator();

           services.AddScoped<ITokenService,TokenService>();
            // 配置事件总线
            services.AddEventExtensionConfiguator(assemblies);
            //配置Quatrz Job
            services.AddQuatrzJobConfigurator();
            //配置传统鉴权JWT
            services.Configure<JWTOptions>(configuration.GetSection("JWTOptions"));
            var options = configuration.GetSection("JWTOptions").Get<JWTOptions>();
            //builder.Services.AddJwtAuthenticationConfiguration( );

            services.AddJwtAuthenticationConfigurationoptions(options);
            return services;
        }
    }
}
