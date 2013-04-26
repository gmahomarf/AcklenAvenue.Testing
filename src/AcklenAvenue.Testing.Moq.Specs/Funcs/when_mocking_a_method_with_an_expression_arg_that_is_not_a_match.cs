using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace AcklenAvenue.Testing.Moq.Specs.Funcs
{
    public class when_mocking_a_method_with_an_expression_arg_that_is_not_a_match
    {
        static Mock<ITestRepository> _mock;
        static TestPerson _result;

        Establish context = () =>
                                {
                                    _mock = new Mock<ITestRepository>();

                                    var sally = new TestPerson {Name = "sally"};

                                    _mock
                                        .Setup(x => x.FindFirstFunc(
                                            ThatHas.AFuncFor<TestPerson>().ThatMatches(sally).
                                                Build()))
                                        .Returns(sally);
                                };
        
        Because of = () => _result = _mock.Object.FindFirstFunc<TestPerson>(x => x.Name == "sam");

        It should_return_null = () => _result.ShouldBeNull();
    }
}