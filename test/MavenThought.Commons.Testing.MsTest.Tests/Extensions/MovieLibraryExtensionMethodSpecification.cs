using System.Collections.Generic;
using System.Linq;
using MavenThought.Commons.Testing.Example;
using Rhino.Mocks;

namespace MavenThought.Commons.Testing.MsTest.Tests.Extensions
{
    /// <summary>
    /// Base specification for testing the Movie Library Extensions
    /// </summary>
    public abstract class MovieLibraryExtensionMethodSpecification
        : ExtensionMethodSpecification<IMovieLibrary>
    {
        /// <summary>
        /// Movie Critic
        /// </summary>
        protected IMovieCritic Critic { get; set; }

        /// <summary>
        /// Setup Basic common needs for IMovieLibrary Extensions
        /// </summary>
        protected override void GivenThat()
        {
            base.GivenThat();

            this.Critic = Mock<IMovieCritic>();

            var movies = new List<IMovie>();
            
            for (var i = 0; i < 10; i++)
            {
                movies.Add(Mock<IMovie>());
            }

            Extended.Stub(ex => ex.Contents).Return(movies);

            Extended.Stub(ex => ex.ListNonViolent()).Return(movies.Take(5));
        }
    }
}