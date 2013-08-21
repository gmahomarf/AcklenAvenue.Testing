using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace AcklenAvenue.Testing.Moq.ExpectedObjects.Specs
{
    public class when_mocking_a_method_that_takes_an_object_that_does_not_match
    {
        static Mock<IWidgetService> _mock;
        static Widget _widgetToUpdate;
        static Widget _updatedWidget;
        static Widget _result;
        static Widget _someOtherWidget;

        Establish context = () =>
                                {
                                    _mock = new Mock<IWidgetService>();

                                    _widgetToUpdate = new Widget {Color = "red"};

                                    _someOtherWidget = new Widget {Color = "yellow"};

                                    _updatedWidget = new Widget {Color = "blue"};

                                    _mock.Setup(x => x.Paint(WithExpected.Object(_widgetToUpdate, AllowAnonymous.No)))
                                        .Returns(_updatedWidget);
                                };

        Because of = () => _result = _mock.Object.Paint(_someOtherWidget);

        It should_return_the_expected_match = () => _result.ShouldBeNull();
    }
}