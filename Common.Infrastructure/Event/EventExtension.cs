using Common.Application.Event;
using Common.Domain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Event
{
    public static class EventExtension
    {
        /// <summary>
        /// 注意实体事件需要和实体在同一个程序集下
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IServiceCollection AddEventExtensionConfiguator(this IServiceCollection services, Assembly[] assembly)
        {
            var handlerTypes = GetEventTypes(assembly, services);
            services.AddSingleton<IEventBus>(sp =>
            {
                var eventManager = new EventManager();
                foreach (var item in handlerTypes)
                {
                    eventManager.AddSubscription(item.Key, item.Value);
                }   
                var serviceProvider = sp.GetRequiredService<IServiceProvider>();
                return new LocalEventBus(eventManager, serviceProvider);
            });
            return services;
        }

        /// <summary>
        /// 获取事件类型和对应的处理器类型
        /// </summary>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        internal static Dictionary<string, Type[]> GetEventTypes(Assembly[] assemblies,IServiceCollection descriptors)
        {
            if (assemblies == null || assemblies.Length == 0)
            {
                throw new ArgumentException("At least one assembly must be provided.", nameof(assemblies));
            }
            var dictionary = new Dictionary<string, Type[]>();
            foreach (var assembly in assemblies)
            {
                //获取所有实现了IEventDomain接口的类型
                var handlerDomains = assembly.GetTypes().Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEventDomain)));

                foreach (var item in handlerDomains)
                {
                    var handlers = assembly.GetTypes()
                   .Where(t => t is { IsAbstract: false, IsInterface: false } && t.IsAssignableTo(typeof(IDomainEventHandler<>).MakeGenericType(item)))
                   .ToArray();
                    foreach (var handler in handlers)
                    {
                        descriptors.AddTransient(handler);
                    }
                   dictionary.Add(item.FullName, handlers);
                }
            }
            return dictionary;
        }
    }
}
