using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AcklenAvenue.Testing.BDD.MSTest
{
    public abstract class SpecificationBase
    {
        public TestContext TestContext { get; set; }

        [ClassInitialize]
        public void BuildSpecificationContext()
        {
            Context();
            BecauseOf();
        }

        [ClassCleanup]
        public void SpecificationCleanup()
        {
            Cleanup();
        }

        [TestInitialize]
        public void InitializeTest()
        {
            BeforeEach();
        }

        [TestCleanup]
        public void CleanupTest()
        {
            BeforeEach();
        }

        protected virtual void BeforeEach()
        {
        }

        protected virtual void AfterEach()
        {
        }

        protected virtual void Context()
        {
        }

        protected virtual void BecauseOf()
        {
        }

        protected virtual void Cleanup()
        {
        }
    }
}