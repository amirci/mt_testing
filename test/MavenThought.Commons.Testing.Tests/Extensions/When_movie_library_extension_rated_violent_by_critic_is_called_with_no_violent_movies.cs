using System.Collections.Generic;
using System.Linq;
using MavenThought.Commons.Testing.Example;
using Rhino.Mocks;

namespace MavenThought.Commons.Testing.Tests.Extensions
{
    /// <summary>
    /// Verifies the behavior of the MoviesRatedViolentByCritic() extension when critic does not
    /// find any violent movies.
    /// </summary>
    public class When_movie_library_extension_rated_violent_by_critic_is_called_with_no_violent_movies
        : MovieLibraryExtensionMethodSpecification
    {
        /// <summary>
        /// Actual Movies returned by SUT
        /// </summary>
        private IEnumerable<IMovie> _actual;
        
        /// <summary>
        /// Movie Critic
        /// </summary>
        protected IMovieCritic Critic { get; set; }

        /// <summary>
        /// Should not find any violent movies
        /// </summary>
        [It]
        public void Should_return_no_movies()
        {
            Assert.AreEqual(0, _actual.Count());
        }

        /// <summary>
        /// Setup the Dependency on IMovieCritic
        /// </summary>
        protected override void GivenThat()
        {
            base.GivenThat();
            Critic = Mock<IMovieCritic>();
            Critic.Stub(mc => mc.IsViolent(Arg<IMovie>.Is.Anything))
                .Return(false);
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