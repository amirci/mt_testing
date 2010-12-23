using MavenThought.Commons.Testing.Example;

namespace MavenThought.Commons.Testing.MsTest.Tests
{
    /// <summary>
    /// Base class for calculator tests
    /// </summary>
    public abstract class CalculatorSpecification 
        : AutoMockSpecificationWithNoContract<Calculator>
    {
    }
}
