using System.Linq;
using MavenThought.Commons.Testing.Example;
using SharpTestsEx;

namespace MavenThought.Commons.Testing.Tests.Extensions
{
    /// <summary>
    /// Tests to ensure that the NonViolentCount() extension returns the correct count.
    /// </summary>
    public class When_movie_library_extension_non_violent_count_is_called
        : MovieLibraryExtensionMethodSpecification
    {
        /// <summary>
        /// Actual Result from Execution
        /// </summary>
        private int _actual;

        /// <summary>
        /// Should only return the count of the NonViolent Movies.
        /// </summary>
        [It]
        public void Should_return_expected_count()
        {
            this.Extended.ListNonViolent().Count().Should().Be(this._actual);
        }

        /// <summary>
        /// Execution the Sut
        /// </summary>
        protected override void WhenIRun()
        {
            _actual = Extended.NonViolentCount();
        }
    }
}