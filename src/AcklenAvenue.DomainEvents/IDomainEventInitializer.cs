using System;

namespace AcklenAvenue.DomainEvents
{
    public interface IDomainEventInitializer
    {
        void WireUpDomainEvents<T>(T obj);
    }
}