using System.Collections.Generic;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace AcklenAvenue.Testing.Moq.Specs.Funcs
{
    public class when_mocking_a_list_method_with_an_expression_arg_that_is_not_a_match
    {
        static Mock<ITestRepository> _mock;
        static IEnumerable<TestPerson> _result;

        Establish context = () =>
                                {
                                    _mock = new Mock<ITestRepository>();

                                    var sally = new TestPerson {Name = "sally"};

                                    _mock
                                        .Setup(x => x.QueryFunc(
                                            ThatHas.AFuncFor<TestPerson>().ThatMatches(sally).
                                                Build()))
                                        .Returns(new List<TestPerson>
                                                     {
                                                         sally
                                                     });
                                };

        Because of = () => _result = _mock.Object.QueryFunc<TestPerson>(x => x.Name == "sam");

        It should_an_empty_list = () => _result.ShouldBeEmpty();
    }
}