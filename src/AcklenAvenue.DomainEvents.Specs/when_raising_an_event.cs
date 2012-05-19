using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace AcklenAvenue.DomainEvents.Specs
{
    public class when_raising_an_event
    {
        static Mock<IEvent> _mockedEvent;
        static Mock<IDispatcher> _mockedDispatcher;

        Establish context = () =>
            {
                _mockedEvent = new Mock<IEvent>();

                _mockedDispatcher = new Mock<IDispatcher>();

                DomainEvent.RegisterDispatcher(_mockedDispatcher.Object);
            };

        Because of = () => DomainEvent.Raise(_mockedEvent.Object);

        It should_dispatch_the_event =
            () => _mockedDispatcher.Verify(x => x.Dispatch(Moq.It.Is<IEvent>(y => y == _mockedEvent.Object)));

        Cleanup when_finished = () => DomainEvent.RegisterDispatcher(null);
    }
}