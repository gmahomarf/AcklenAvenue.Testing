using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace AcklenAvenue.DomainEvents.Specs
{
    public class when_raising_an_event
    {
        static Mock<IDispatcher> _mockedDispatcher;

        Establish context = () =>
            {
                _event = new object();

                _mockedDispatcher = new Mock<IDispatcher>();

                DomainEvent.RegisterDispatcher(_mockedDispatcher.Object);
            };

        Because of = () => DomainEvent.Raise(_event);

        It should_dispatch_the_event =
            () => _mockedDispatcher.Verify(x => x.Dispatch(Moq.It.Is<object>(y => y == _event)));

        Cleanup when_finished = () => DomainEvent.RegisterDispatcher(null);
        static object _event;
    }
}