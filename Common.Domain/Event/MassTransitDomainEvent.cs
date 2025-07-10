namespace Common.Domain;

public interface  IMassTransitDomainEvent
{
    public DateTime OccurredOnUtc { get;}
}