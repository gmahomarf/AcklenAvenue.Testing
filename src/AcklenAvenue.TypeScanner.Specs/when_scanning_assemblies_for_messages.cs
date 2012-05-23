using System;
using System.Collections.Generic;
using Machine.Specifications;

namespace AcklenAvenue.TypeScanner.Specs
{
    public class when_scanning_assemblies_for_messages
    {
        static ITypeScanner _messageTypeScanner;
        static List<Type> _result;

        Establish context = () =>
                                {
                                    _messageTypeScanner = new TypeScanner();
                                };

        Because of = () => _result = _messageTypeScanner.GetTypesOf<object>();

        It should_return_a_list_of_message_types = () => _result.Count.ShouldBeGreaterThan(0);
    }
}