using Common.Domain;


namespace Common.Infrastructure.Event
{
    public abstract class BaseDomainEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent>
    where TDomainEvent : IEventDomain
    {
        public abstract Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken = default);

        public Task Handle(IEventDomain domainEvent, CancellationToken cancellationToken = default) =>
            Handle((TDomainEvent)domainEvent, cancellationToken);
    }
}
