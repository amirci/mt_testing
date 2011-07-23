using System.Collections.Generic;
using MavenThought.Commons.Testing.Example;
using Rhino.Mocks;

namespace MavenThought.Commons.Testing.Tests.Extensions
{
    /// <summary>
    /// Verifies the behavior of the MoviesRatedViolentByCritic() extension, when critic declares movies
    /// as violent.
    /// </summary>
    public class When_movie_library_extension_rated_violent_by_critic_is_called_with_violent_movies 
        : MovieLibraryExtensionMethodSpecification
    {
        /// <summary>
        /// Actual movies returned
        /// </summary>
        private IEnumerable<IMovie> _actual;

        /// <summary>
        /// Movie Critic
        /// </summary>
        protected IMovieCritic Critic { get; set; }

        /// <summary>
        /// Verifies that critic found all movies violent
        /// </summary>
        [It]
        public void Should_return_all_movies_as_violent()
        {
            Assert.AreElementsSameIgnoringOrder(Extended.Contents, _actual);
        }

        /// <summary>
        /// Sets up the dependency on IMovieCritic
        /// </summary>
        protected override void GivenThat()
        {
            base.GivenThat();
            Critic = Mock<IMovieCritic>();
            Critic.Stub(mc => mc.IsViolent(Arg<IMovie>.Is.Anything))
                .Return(true);
        }

        /// <summary>
        /// Executes the SUT
        /// </summary>
        protected override void WhenIRun()
        {
            _actual = Extended.MoviesRatedViolentByCritic(Critic);
        }
    }
}