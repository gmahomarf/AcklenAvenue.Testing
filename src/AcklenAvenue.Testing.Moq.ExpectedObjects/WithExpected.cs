using System;
using ExpectedObjects;
using Moq;

namespace AcklenAvenue.Testing.Moq.ExpectedObjects
{
    public static class WithExpected
    {
        public static T Object<T>(T expectedObject, AllowAnonymous allowAnonymous = AllowAnonymous.No)
        {
            return Match.Create<T>(actual =>
                                       {
                                           try
                                           {
                                               if(allowAnonymous == AllowAnonymous.Yes)
                                               {
                                                   expectedObject.ToExpectedObject().IgnoreTypes().ShouldEqual(actual);
                                                   return true;
                                               }

                                               expectedObject.ToExpectedObject().ShouldEqual(actual);
                                               return true;
                                           }
                                           catch (Exception ex)
                                           {
                                               Console.WriteLine(
                                                   "The mock constraint failed because an unexpected object was passed to the mock at runtime.\r\n" +
                                                   ex.Message);
                                               return false;
                                           }
                                       });
        }
    }
}