using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;

namespace AcklenAvenue.Linq2Props.Specs
{
    public class when_searching_properties_for_a_match
    {
        Establish context = () =>
            {
                _object = new TestClass();
            };

        Because of = () => _result = _object.Members().Where(x => x.GetType() == typeof (TestClass));

        It should_return_the_expected_list_of_properties = () => { };
        static TestClass _object;
    }
}
