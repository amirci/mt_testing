using System.Collections.Generic;
using System.Linq;
using MavenThought.Commons.Testing.Example;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;

namespace MavenThought.Commons.Testing.MsTest.Tests.Extensions
{
    /// <summary>
    /// Verifies the behavior of the MoviesRatedViolentByCritic() extension when critic does not
    /// find any violent movies.
    /// </summary>
    [TestClass]
    public class When_movie_library_extension_rated_violent_by_critic_is_called_with_no_violent_movies
        : MovieLibraryExtensionMethodSpecification
    {
        /// <summary>
        /// Actual Movies returned by SUT
        /// </summary>
        private IEnumerable<IMovie> _actual;

        /// <summary>
        /// Should not find any violent movies
        /// </summary>
        [TestMethod]
        public void Should_return_no_movies()
        {
            this._actual.Count().Should().Be(0);
        }

        /// <summary>
        /// Execute the SUT
        /// </summary>
        protected override void WhenIRun()
        {
            _actual = Extended.MoviesRatedViolentByCritic(Critic);
        }
    }
}