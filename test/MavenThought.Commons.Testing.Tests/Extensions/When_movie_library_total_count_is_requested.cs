using System.Linq;
using MavenThought.Commons.Testing.Example;
using SharpTestsEx;

namespace MavenThought.Commons.Testing.Tests.Extensions
{
    /// <summary>
    /// Tests the TotalCount() Extension for correct behavior
    /// </summary>
    public class When_movie_library_total_count_is_requested 
        : MovieLibraryExtensionMethodSpecification
    {
        /// <summary>
        /// Actual Count Returned by SUT
        /// </summary>
        private int _actual;

        /// <summary>
        /// Verifies that the Results from the TotalCount() match up to the total count of movies in the extended
        /// library
        /// </summary>
        [It]
        public void Should_return_a_total_count_of_the_movie_library_contents()
        {
            this.Extended.Contents.Count().Should().Be(_actual);
        }

        /// <summary>
        /// Executes Extension
        /// </summary>
        protected override void WhenIRun()
        {
            _actual = Extended.TotalCount();
        }
    }
}