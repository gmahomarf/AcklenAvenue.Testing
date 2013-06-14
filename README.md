AcklenAvenue.Testing
====================


Here's a helpful "live template" (ReSharper) that you can use with the BDD.MSTest extentions:

`[TestClass]
public class when_$something_happens$ : SpecificationBase
{
    protected override void Context()
    {
        $build_your_SUT_here$
    }

    protected override void BecauseOf()
    {
        _result = null;
    }

    [TestMethod]
    public void it_should_$do_something$()
    {
        _result.ShouldEqual(Something);
    }
}`
