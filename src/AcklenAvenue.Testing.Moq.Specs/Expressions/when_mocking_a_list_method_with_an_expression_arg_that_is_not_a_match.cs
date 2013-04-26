using System.Collections.Generic;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace AcklenAvenue.Testing.Moq.Specs.Expressions
{
    public class when_mocking_a_list_method_with_an_func_arg_that_is_not_a_match
    {
        static Mock<ITestRepository> _mock;
        static IEnumerable<TestPerson> _result;

        Establish context = () =>
                                {
                                    _mock = new Mock<ITestRepository>();

                                    var sally = new TestPerson {Name = "sally"};

                                    _mock
                                        .Setup(x => x.Query(
                                            ThatHas.AnExpressionFor<TestPerson>().ThatMatches(sally).
                                                Build()))
                                        .Returns(new List<TestPerson>
                                                     {
                                                         sally
                                                     });
                                };

        Because of = () => _result = _mock.Object.Query<TestPerson>(x => x.Name == "sam");

        It should_return_an_empty_list = () => _result.ShouldBeEmpty();
    }
}