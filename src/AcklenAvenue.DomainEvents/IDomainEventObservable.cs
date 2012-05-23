namespace AcklenAvenue.DomainEvents
{
    public interface IDomainEventObservable
    {
        event DomainEvent NotifyObservers;
    }
}