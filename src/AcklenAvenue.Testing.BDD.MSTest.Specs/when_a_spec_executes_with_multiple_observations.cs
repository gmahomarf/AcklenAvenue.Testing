using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AcklenAvenue.Testing.BDD.MSTest.Specs
{
    [TestClass]
    public class when_a_spec_executes_with_multiple_observations : SpecificationBase
    {
        static int _contextExecutionCount;

        protected override void Context()
        {
            _contextExecutionCount++;
        }

        [TestMethod]
        public void it_should_only_run_the_context_once()
        {
            _contextExecutionCount.ShouldEqual(1);
        }

        [TestMethod]
        public void it_should_still_only_run_the_context_once()
        {
            _contextExecutionCount.ShouldEqual(1);
        }

        [TestMethod]
        public void it_should_yet_only_run_the_context_once()
        {
            _contextExecutionCount.ShouldEqual(1);
        }
    }
}