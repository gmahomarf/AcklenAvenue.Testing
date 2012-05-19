using Machine.Specifications;
using Moq;
using StructureMap;
using It = Machine.Specifications.It;

namespace AcklenAvenue.DomainEvents.StructureMap.Specs
{
    public class when_dispatching_an_event_with_handler_registered_in_the_container
    {
        static Container _container;
        static Mock<IEventHandler<object>> _handler;
        static IDispatcher _dispatcher;
        static Mock<object> _event;

        Establish context = () =>
            {
                _container = new Container();
                _handler = new Mock<IEventHandler<object>>();
                _container.Configure(x => x.For<IEventHandler<object>>().Use(_handler.Object));

                _event = new Mock<object>();

                _dispatcher = new StructureMapDispatcher(_container);
            };

        Because of = () => _dispatcher.Dispatch(_event.Object);

        It should_dispatch_the_event_using_the_expected_handler =
            () => _handler.Verify(x => x.Handle(Moq.It.Is<object>(y => y == _event.Object)));
    }
}