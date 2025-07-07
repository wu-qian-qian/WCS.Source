using MassTransit;

namespace Common.Infrastructure.Event
{
    /// <summary>
    /// 公共事件接口
    /// </summary>
    /// <typeparam name="TIntegrationEvent"></typeparam>
    public abstract class IntegrationEventConsumer<TIntegrationEvent>
    : IConsumer<TIntegrationEvent>
    where TIntegrationEvent : MassTransitEvent
    {
        public abstract Task Consume(ConsumeContext<TIntegrationEvent> context);
    }
}
