using Common.Application.Event;
using Common.Domain;


namespace Common.Infrastructure.Event
{
    public abstract class MassTransitEvent : IEventDomain
    {
        protected MassTransitEvent(Guid id, DateTime occurredOnUtc)
        {
            Id = id;
            OccurredOnUtc = occurredOnUtc;
        }

        public Guid Id { get; init; }

        public DateTime OccurredOnUtc { get; init; }
    }
}
