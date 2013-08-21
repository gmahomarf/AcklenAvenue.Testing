using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AcklenAvenue.Testing.BDD.MSTest
{
    [TestClass]
    public abstract class SpecificationBase
    {
        static int _observationsTownDown;
        static readonly IDictionary<string, int> SpecificationsInMemory = new Dictionary<string, int>();

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void BuildSpecificationContext()
        {
            bool specInMemory = SpecificationsInMemory.ContainsKey(GetType().ToString());
            if (specInMemory) return;

            Context();
            BecauseOf();

            int observationCount =
                GetType().GetMethods().Count(
                    x => x.GetCustomAttributes(true).Any(y => y is TestMethodAttribute));

            SpecificationsInMemory.Add(GetType().ToString(), observationCount);
        }

        [TestCleanup]
        public void SpecificationTeardown()
        {
            var count = 0;
            if (SpecificationsInMemory.TryGetValue(GetType().ToString(), out count))
            {
                _observationsTownDown++;

                if (_observationsTownDown == count)
                {
                    Cleanup();
                    SpecificationsInMemory.Remove(GetType().ToString());
                }
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