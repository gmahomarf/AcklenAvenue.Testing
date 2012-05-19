using System;

namespace AcklenAvenue.DomainEvents
{
    public class NoHandlerAvailable<T> : Exception
    {
        public NoHandlerAvailable() : base("There were no handlers available for '" + typeof(T) + "'.")
        {            
        }
    }
}