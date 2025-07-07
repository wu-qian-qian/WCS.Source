using Common.Application.Event;
using Common.Domain;

namespace Common.Infrastructure.Event
{
  
    public interface IDomainEventHandler<in TDomainEvent> : IDomainEventHandler
    where TDomainEvent : IEventDomain
    {
        Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken = default);
    }

    public interface IDomainEventHandler
    {
        Task Handle(IEventDomain domainEvent, CancellationToken cancellationToken = default);
    }

}
