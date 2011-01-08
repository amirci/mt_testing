using System;
using Xunit;
using Rhino.Mocks;

namespace MavenThought.Commons.Testing
{
    /// <summary>
    /// Base test with mocking
    /// </summary>
    public abstract class BaseTest : IDisposable
    {
        /// <summary>
        /// Mocks an object of type U
        /// </summary>
        /// <param name="arguments">
        /// Arguments for the mock
        /// </param>
        /// <typeparam name="U">
        /// Type of the mock
        /// </typeparam>
        /// <returns>
        /// A mock instance of U created with arguments
        /// </returns>
        public static U Mock<U>(params object[] arguments) where U : class
        {
            return MockRepository.GenerateMock<U>(arguments);
        }

        /// <summary>
        /// Utility method to deduce the type from the first argument
        /// </summary>
        /// <typeparam name="U">Type of the mocked target</typeparam>
        /// <param name="justForTheType">Ignored argument</param>
        /// <param name="arguments">Arguments to pass to the mock</param>
        /// <returns>A mock instance of U created with arguments</returns>
        public static U MockIt<U>(U justForTheType, params object[] arguments) where U : class
        {
            return Mock<U>(arguments);
        }

        /// <summary>
        /// Creates a dynamic mock with the specified type
        /// </summary>
        /// <param name="typeOfU">Type of the object</param>
        /// <returns>An instance of the type</returns>
        public static object Mock(Type typeOfU)
        {
            return new MockRepository().DynamicMock(typeOfU);
        }

        /// <summary>
        /// Generates a stub of type U
        /// </summary>
        /// <param name="arguments">
        /// Arguments for the mock
        /// </param>
        /// <typeparam name="U">
        /// Type of the stub
        /// </typeparam>
        /// <returns>
        /// The result of GenerateStub
        /// </returns>
        public U Stub<U>(params object[] arguments) where U : class
        {
            return MockRepository.GenerateStub<U>(arguments);
        }

        /// <summary>
        /// Generates a partial mock of Type U
        /// </summary>
        /// <typeparam name="U">Type to mock</typeparam>
        /// <param name="arguments">Arguments for the constructor</param>
        /// <returns>A partial mock of U</returns>
        public U Partial<U>(params object[] arguments) where U : class
        {
            return MockRepository.GeneratePartialMock<U>(arguments);
        }

        /// <summary>
        /// Setup before all tests
        /// </summary>
        [Obsolete("Use IFixture<T> instead: http://xunit.codeplex.com/wikipage?title=Comparisons&referringTitle=HowToUse#note3", true)]
        public void FixtureSetUp()
        {
            this.BeforeAllTests();
        }

        /// <summary>
        /// Create the TSUT before each test
        /// </summary>
        public BaseTest()
        {
            this.BeforeEachTest();
        }

        /// <summary>
        /// Clean after each test
        /// </summary>
        public virtual void Dispose()
        {
            this.AfterEachTest();
        }

        /// <summary>
        /// Clean up after all tests run
        /// </summary>
        [Obsolete("Use IFixture<T> instead: http://xunit.codeplex.com/wikipage?title=Comparisons&referringTitle=HowToUse#note3", true)]
        public void FixtureTearDown()
        {
            this.AfterAllTests();
        }

        /// <summary>
        /// Placeholder to run before all tests
        /// </summary>
        protected virtual void BeforeAllTests()
        {
        }

        /// <summary>
        /// Place holder for optional initialization before each test
        /// </summary>
        protected virtual void BeforeEachTest()
        {
        }

        /// <summary>
        /// Placeholder to cleanup after each test
        /// </summary>
        protected virtual void AfterEachTest()
        {
        }

        /// <summary>
        /// Placeholder to run after all test run
        /// </summary>
        protected virtual void AfterAllTests()
        {
        }
    }
}