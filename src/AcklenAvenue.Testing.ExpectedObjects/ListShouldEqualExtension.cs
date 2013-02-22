using System;
using System.Collections.Generic;
using System.Linq;
using ExpectedObjects;

namespace AcklenAvenue.Testing.ExpectedObjects
{
    public static class ListShouldEqualExtension
    {
        public static void ShouldEqual<T>(this IEnumerable<T> actual, IEnumerable<T> expected)
        {
            var expectedList = expected.ToList();
            var actualList = actual.ToList();

            var expectedCount = expectedList.Count;
            var actualCount = actualList.Count;

            if (expectedCount != actualCount)
                throw new Exception(string.Format("Expected list of size {0} but found list of size {1}", expectedCount,
                                                  actualCount));

            for (int i = 0; i < expectedCount; i++ )
            {
                var expectedObject = expectedList[i];
                var actualObject = actualList[i];

                expectedObject.ToExpectedObject().ShouldEqual(actualObject);
            }
        }
    }
}