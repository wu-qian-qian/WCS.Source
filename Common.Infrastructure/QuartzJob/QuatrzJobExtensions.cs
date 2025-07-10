using Common.Domain;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Common.Infrastructure.QuartzJobs;

public static class QuatrzJobExtensions
{
    public static IServiceCollection AddQuatrzJobConfigurator(this IServiceCollection services)
    {
        services.AddSingleton<QuartzJobListener>();
        services.AddQuartz(configurator =>
        {
            var scheduler = Guid.NewGuid();
            configurator.SchedulerId = $"default-id-{scheduler}";
            configurator.SchedulerName = $"default-name-{scheduler}";
            configurator.MisfireThreshold = TimeSpan.FromSeconds(8);
            configurator.InterruptJobsOnShutdownWithWait = true;
            configurator.UseDefaultThreadPool(tp =>
            {
                tp.MaxConcurrency = 10; // 设置最大并发数
            });
            configurator.AddSchedulerListener<QuartzJobListener>();
        });
        services.AddQuartzHostedService(configure =>
        {
            configure.AwaitApplicationStarted = true; // 等待应用程序启动完成后再启动Quartz
        });
        return services;
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="TJob"></typeparam>
    /// <param name="options"></param>
    /// <param name="quartzJobOptins"></param>
    public static void AddQuqartzJob<TJob>(this QuartzOptions options, QuartzJobOptins quartzJobOptins)
        where TJob : IJob
    {
        var jobName = typeof(TJob).FullName!;
        options
            .AddJob<TJob>(configure =>
            {
                configure.WithIdentity(jobName);
                var detail = configure.Build();
                var keyValues = new JobDataMap();
                keyValues.Add(quartzJobOptins.TimeoutSeconds, quartzJobOptins.TimeOut);
                configure.UsingJobData(keyValues);
            })
            .AddTrigger(configure =>
            {
                configure
                    .ForJob(jobName)
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInSeconds(3).RepeatForever());
            });
    }
}