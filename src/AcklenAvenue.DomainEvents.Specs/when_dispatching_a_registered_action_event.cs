using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace AcklenAvenue.DomainEvents.Specs
{
    public class when_dispatching_a_registered_action_event
    {
        static ActionEventDispatcher _dispatcher;
        static Mock<IEvent> _event;
        static IEvent _eventDispatched;

        Establish context = () =>
            {
                _dispatcher = new ActionEventDispatcher();

                _dispatcher.Register<IEvent>(x => { _eventDispatched = x; });

                _event = new Mock<IEvent>();
            };

        Because of = () => _dispatcher.Dispatch(_event.Object);

        It should_handle_the_event_using_the_provided_action = () => _eventDispatched.ShouldEqual(_event.Object);
    }
}