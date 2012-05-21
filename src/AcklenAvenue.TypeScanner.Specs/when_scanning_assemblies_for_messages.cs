using System;
using System.Collections.Generic;
using Machine.Specifications;

namespace AcklenAvenue.TypeScanner.Specs
{
    public class when_scanning_assemblies_for_messages
    {
        static ITypeScanner<object> _messageTypeScanner;
        static List<Type> _result;

        Establish context = () =>
                                {
                                    _messageTypeScanner = new TypeScanner<object>();
                                };

        Because of = () => _result = _messageTypeScanner.GetTypes();

        It should_return_a_list_of_message_types = () => _result.Count.ShouldBeGreaterThan(0);
    }
}