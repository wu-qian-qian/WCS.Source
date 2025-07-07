using Common.Application.Event;
using Common.Domain;
using MassTransit;

namespace Common.Infrastructure.Event
{
    internal sealed class EventBus(IBus bus) : IEventBus
    {
        public async Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default)
            where T : IEventDomain
        {
            await bus.Publish(integrationEvent, cancellationToken);
        }
    }
}
