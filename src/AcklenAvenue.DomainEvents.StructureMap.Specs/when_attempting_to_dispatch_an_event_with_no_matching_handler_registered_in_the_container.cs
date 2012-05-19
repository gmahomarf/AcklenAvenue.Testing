using System;
using Machine.Specifications;
using Moq;
using StructureMap;
using It = Machine.Specifications.It;

namespace AcklenAvenue.DomainEvents.StructureMap.Specs
{
    public class when_attempting_to_dispatch_an_event_with_no_matching_handler_registered_in_the_container
    {
        static Container _container;        
        static IDispatcher _dispatcher;
        static Mock<IEvent> _event;
        static Exception _exception;

        Establish context = () =>
            {
                _container = new Container();
                
                _event = new Mock<IEvent>();

                _dispatcher = new StructureMapDispatcher(_container);
            };

        Because of = () => _exception = Catch.Exception(() => _dispatcher.Dispatch(_event.Object));

        It should_the_the_expected_exception = () => _exception.ShouldBeOfType<NoHandlerAvailable<IEvent>>();
    }
}