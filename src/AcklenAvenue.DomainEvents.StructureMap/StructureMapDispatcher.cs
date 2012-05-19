using System.Linq;
using StructureMap;

namespace AcklenAvenue.DomainEvents.StructureMap
{
    public class StructureMapDispatcher : IDispatcher
    {
        readonly IContainer _container;

        public StructureMapDispatcher(IContainer container)
        {
            _container = container;
        }

        #region IDispatcher Members

        public void Dispatch<T>(T @event)
        {
            var eventHandlers = _container.GetAllInstances<IEventHandler<T>>().ToList();
            if(!eventHandlers.Any())
            {
                throw new NoHandlerAvailable<T>();
            }
            eventHandlers
                .ForEach(handler => handler.Handle(@event));
        }

        #endregion
    }
}