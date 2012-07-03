using System;
using System.Collections.Generic;
using ExpectedObjects;

namespace AcklenAvenue.Testing.ExpectedObjects
{
    public static class ListShouldEqualExtension
    {
        public static void ShouldEqual<T>(this IList<T> expectedList, IList<T> actualList)
        {
            var expectedCount = expectedList.Count;
            var actualCount = actualList.Count;

            if (expectedCount != actualCount)
                throw new Exception(string.Format("Expected list of size {0} but found list of size {1}", expectedCount,
                                                  actualCount));

            for (int i = 0; i < expectedCount; i++ )
            {
                var expectedObject = expectedList[i];
                var actualObject = actualList[i];

                expectedObject.ToExpectedObject().ShouldEqual<T>(actualObject);
            }
        }
    }
}