using NUnit.Framework;

namespace AcklenAvenue.Testing.BDD.NUnit
{
    public abstract class Specification
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            Given();
            When();
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            Cleanup();
        }

        protected virtual void Given() { }
        protected virtual void When() { }
        protected virtual void Cleanup() { }
    }
}
