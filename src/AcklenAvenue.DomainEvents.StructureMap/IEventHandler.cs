namespace AcklenAvenue.DomainEvents.StructureMap
{
    public interface IEventHandler<in T> where T : IEvent
    {
        void Handle(T @event);
    }
}