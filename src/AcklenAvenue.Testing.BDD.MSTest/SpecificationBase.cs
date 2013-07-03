using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AcklenAvenue.Testing.BDD.MSTest
{
    [TestClass]
    public abstract class SpecificationBase
    {
        static int _observationCount;
        static int _observationsTownDown;
        static bool _specificationExecuting;

        protected SpecificationBase()
        {
            int observations =
                GetType().GetMethods().Count(
                    x => x.GetCustomAttributes(true).Any(y => y is TestMethodAttribute));

            _observationCount = observations;
        }

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void BuildSpecificationContext()
        {
            if (_specificationExecuting) return;

            _specificationExecuting = true;
            Context();
            BecauseOf();
        }

        [TestCleanup]
        public void SpecificationTeardown()
        {
            _observationsTownDown++;

            if (_observationsTownDown == _observationCount)
            {
                Cleanup();
                _specificationExecuting = false;
            }
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