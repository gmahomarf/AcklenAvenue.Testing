using System.Collections.Generic;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace AcklenAvenue.Testing.Moq.Specs.Funcs
{
    public class when_mocking_a_method_with_an_expression_arg_that_is_a_match
    {
        const string Name = "sally";
        static Mock<ITestRepository> _mock;
        static IEnumerable<TestPerson> _result;
        static List<TestPerson> _expectedTestPersons;

        Establish context = () =>
                                {
                                    _mock = new Mock<ITestRepository>();

                                    var sally = new TestPerson {Name = Name};

                                    _expectedTestPersons = new List<TestPerson>
                                                               {
                                                                   sally
                                                               };

                                    _mock
                                        .Setup(x => x.QueryFunc(
                                            ThatHas.AFuncFor<TestPerson>().ThatMatches(sally).
                                                Build()))
                                        .Returns(_expectedTestPersons);
                                };

        Because of = () => _result = _mock.Object.QueryFunc<TestPerson>(x => x.Name == Name);
                         
        It should_return_a_list_of_matches = () => _result.ShouldBeLike(_expectedTestPersons);
    }
}