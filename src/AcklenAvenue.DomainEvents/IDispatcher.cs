using System;

namespace AcklenAvenue.DomainEvents
{
    public interface IDispatcher
    {
        void Dispatch<T>(T @event);        
    }
}