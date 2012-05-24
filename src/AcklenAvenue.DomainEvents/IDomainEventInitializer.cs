using System;

namespace AcklenAvenue.DomainEvents
{
    public interface IDomainEventInitializer
    {
        void WireUpDomainEvent<T>(T obj);
    }
}