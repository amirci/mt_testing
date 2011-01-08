using Xunit;

namespace MavenThought.Commons.Testing
{
    /// <summary>
    /// Base test with a system under tests
    /// </summary>
    /// <typeparam name="TSUT">Type of the system under test</typeparam>
    /// <remarks>The order of the calls is the following
    /// <list>
    ///    <item>BeforeCreateSut</item>
    ///    <item>CreateSut</item>
    ///    <item>AfterCreateSut</item>
    /// </list>
    /// </remarks>
    public abstract class BaseTestWithSut<TSUT>
        : BaseTest
    {
        /// <summary>
        /// Gets or sets the system under test
        /// </summary>
        protected TSUT Sut { get; set; }

        /// <summary>
        /// Create the TSUT before each test
        /// </summary>
        // xunit: setup is implied via the c'tor [SetUp]
        public BaseTestWithSut() : base()
        {
            this.BeforeCreateSut();

            this.Sut = this.CreateSut();

            this.AfterCreateSut();
        }

        /// <summary>
        /// Place holder to add code before creating the TSUT
        /// </summary>
        protected virtual void BeforeCreateSut()
        {
        }

        /// <summary>
        /// Place holder to add code after creating the TSUT
        /// </summary>
        protected virtual void AfterCreateSut()
        {
        }

        /// <summary>
        /// Create the TSUT
        /// </summary>
        /// <returns>An instance of TSUT</returns>
        protected abstract TSUT CreateSut();
    }
}