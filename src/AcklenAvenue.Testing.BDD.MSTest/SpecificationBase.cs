using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AcklenAvenue.Testing.BDD.MSTest
{
    public abstract class SpecificationBase
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void BuildSpecificationContext()
        {
            Context();
            BecauseOf();
        }

        [TestCleanup]
        public void SpecificationCleanup()
        {
            Cleanup();            
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