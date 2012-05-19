namespace AcklenAvenue.DomainEvents
{
    public static class DomainEvent
    {
        static IDispatcher _dispatcher;

        public static void Raise<T>(T @event)
        {
            if (_dispatcher == null)
            {
                throw new NoDispatcherException();
            }
            _dispatcher.Dispatch(@event);
        }

        public static void RegisterDispatcher(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
    }
}