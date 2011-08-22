using Rhino.Mocks;

namespace MavenThought.Commons.Testing
{
    /// <summary>
    /// Specification for testing extension methods for a specific type.
    /// </summary>
    /// <typeparam name="TExtended">Type Extended by SUT</typeparam>
    public abstract class ExtensionMethodSpecification<TExtended>
        : BaseSpecification 
        where TExtended : class
    {
        /// <summary>
        /// Type being Extended by the SUT
        /// </summary>
        protected TExtended Extended { get; private set; }

        /// <summary>
        /// Default setup for Creating an instance of the extended type is to use a mock.
        /// Override to provide alternative method for creating the extended type.
        /// </summary>
        /// <returns>An instance of the Extended Type</returns>
        protected virtual TExtended CreateExtended()
        {
            return Mock<TExtended>();
        }

        /// <summary>
        /// Generates a mock for the extended type before each test
        /// </summary>
        protected override void GivenThat()
        {
            base.GivenThat();
            Extended = CreateExtended();
        }
    }
}