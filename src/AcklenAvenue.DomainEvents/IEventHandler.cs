namespace AcklenAvenue.DomainEvents
{
    public interface IEventHandler<in T> 
    {
        void Handle(T @event);
    }
}