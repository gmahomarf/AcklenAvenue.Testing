using System;

namespace AcklenAvenue.DomainEvents
{
    public class NoDispatcherException : Exception
    {
        public NoDispatcherException() : base("There was no dispatcher registered in the DomainEvent class.")
        {
        }
    }
}