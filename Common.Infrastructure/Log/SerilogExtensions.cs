using Microsoft.AspNetCore.Builder;
using Serilog;


namespace Common.Infrastructure.Log
{
    public static class SerilogExtensions
    {
        public static WebApplicationBuilder AddSerilogConfigurator(this WebApplicationBuilder builder)
        {
            //配置日志
            Serilog.Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information().MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Fatal)
                .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
                .WriteTo.File("logs/log.txt", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
            .CreateLogger();

            //这里用替换原生日志
            builder.Host.UseSerilog();
            return builder;
        }
        public static WebApplication UseSerilogRequest(this WebApplication app)
        {
            //使用Serilog中间件
            app.UseSerilogRequestLogging();
            return app;
        }
    }
}
