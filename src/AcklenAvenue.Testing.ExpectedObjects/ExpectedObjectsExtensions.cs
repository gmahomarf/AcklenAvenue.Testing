using ExpectedObjects;

namespace AcklenAvenue.Testing.ExpectedObjects
{
    public static class ExpectedObjectsExtensions
    {
        public static void Like(this object actual, object expected)
        {
            expected.ToExpectedObject().ShouldEqual(actual);
        }

        public static void IsExpectedToBeLike(this object actual, object expected)
        {
            expected.ToExpectedObject().ShouldEqual(actual);
        }

        public static void IsExpectedToBeSimilar(this object actual, object expected)
        {
            expected.ToExpectedObject().IgnoreTypes().ShouldEqual(actual);
        }
    }
}