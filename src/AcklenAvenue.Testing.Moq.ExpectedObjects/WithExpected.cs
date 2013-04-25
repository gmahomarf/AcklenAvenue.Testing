using System;
using ExpectedObjects;
using Moq;

namespace AcklenAvenue.Testing.Moq.ExpectedObjects
{
    public static class WithExpected
    {
        public static T Object<T>(T expectedObject)
        {
            return Match.Create<T>(x =>
                                       {
                                           var actual = x.ToExpectedObject();
                                           try
                                           {
                                               actual.ShouldEqual(expectedObject);
                                               return true;
                                           }
                                           catch (Exception ex)
                                           {
                                               throw new Exception(
                                                   "The mock constraint failed because an unexpected object was passed to the mock at runtime.",
                                                   ex);
                                           }
                                       });
        }
    }
}