namespace AcklenAvenue.DomainEvents.StructureMap
{
    public interface IEventHandler<in T> 
    {
        void Handle(T @event);
    }
}