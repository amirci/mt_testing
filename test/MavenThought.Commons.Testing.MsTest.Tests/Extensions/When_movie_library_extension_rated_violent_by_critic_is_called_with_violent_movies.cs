using System.Collections.Generic;
using MavenThought.Commons.Testing.Example;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using SharpTestsEx;

namespace MavenThought.Commons.Testing.MsTest.Tests.Extensions
{
    /// <summary>
    /// Verifies the behavior of the MoviesRatedViolentByCritic() extension, when critic declares movies
    /// as violent.
    /// </summary>
    [TestClass]
    public class When_movie_library_extension_rated_violent_by_critic_is_called_with_violent_movies 
        : MovieLibraryExtensionMethodSpecification
    {
        /// <summary>
        /// Actual movies returned
        /// </summary>
        private IEnumerable<IMovie> _actual;

        /// <summary>
        /// Verifies that critic found all movies violent
        /// </summary>
        [TestMethod]
        public void Should_return_all_movies_as_violent()
        {
            this.Extended.Contents.Should().Have.SameValuesAs(this._actual);
        }

        /// <summary>
        /// Sets up the dependency on IMovieCritic
        /// </summary>
        protected override void GivenThat()
        {
            base.GivenThat();
            
            this.Critic = Mock<IMovieCritic>();
            
            this.Critic.Stub(mc => mc.IsViolent(Arg<IMovie>.Is.Anything)).Return(true);
        }

        /// <summary>
        /// Executes the SUT
        /// </summary>
        protected override void WhenIRun()
        {
            this._actual = Extended.MoviesRatedViolentByCritic(this.Critic);
        }
    }
}