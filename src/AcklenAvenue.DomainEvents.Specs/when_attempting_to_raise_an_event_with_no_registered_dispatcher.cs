using System;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace AcklenAvenue.DomainEvents.Specs
{
    public class when_attempting_to_raise_an_event_with_no_registered_dispatcher
    {
        static Mock<IEvent> _event;
        static Exception _exception;

        Establish context = () => { _event = new Mock<IEvent>(); };

        Because of = () => _exception = Catch.Exception(() => DomainEvent.Raise(_event.Object));

        It should_throw_the_expected_exception = () => _exception.ShouldBeOfType<NoDispatcherException>();

        Cleanup when_finished = () => DomainEvent.RegisterDispatcher(null);
    }    
}