using System;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace AcklenAvenue.DomainEvents.Specs
{    
    public class when_attempting_to_dispatch_an_event_without_a_matching_handler
    {
        static ActionEventDispatcher _dispatcher;
        static Mock<object> _event;
        static Exception _exception;

        Establish context = () =>
            {
                _dispatcher = new ActionEventDispatcher();

                _event = new Mock<object>();
            };

        Because of = () => _exception = Catch.Exception(() => _dispatcher.Dispatch(_event.Object));

        It should_the_the_expected_exception = () => _exception.ShouldBeOfType<NoHandlerAvailable<object>>();        
    }
}