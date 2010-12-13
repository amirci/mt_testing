namespace MavenThought.Commons.Testing
{
    /// <summary>
    /// Specification for a Sut with no contract
    /// </summary>
    /// <typeparam name="TSut">System under test to use</typeparam>
    public abstract class AutoMockSpecificationWithNoContract<TSut>
        : AutoMockSpecification<TSut, TSut> where TSut : class
    {
    }
}