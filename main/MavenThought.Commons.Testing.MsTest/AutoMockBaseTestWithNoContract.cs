namespace MavenThought.Commons.Testing
{
    /// <summary>
    /// Auto mock base test when the class has no contract
    /// </summary>
    /// <typeparam name="TSut">Type of the system under test</typeparam>
    public abstract class AutoMockBaseTestWithNoContract<TSut>
        : AutoMockBaseTest<TSut, TSut> where TSut : class
    {
    }
}