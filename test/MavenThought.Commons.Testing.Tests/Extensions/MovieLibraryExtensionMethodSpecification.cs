using System.Collections.Generic;
using System.Linq;
using MavenThought.Commons.Testing.Example;
using Rhino.Mocks;

namespace MavenThought.Commons.Testing.Tests.Extensions
{
    /// <summary>
    /// Base specification for testing the Movie Library Extensions
    /// </summary>
    public abstract class MovieLibraryExtensionMethodSpecification
        : ExtensionMethodSpecification<IMovieLibrary>
    {
        /// <summary>
        /// Setup Basic common needs for IMovieLibrary Extensions
        /// </summary>
        protected override void GivenThat()
        {
            base.GivenThat();

            var movies = new List<IMovie>();
            
            for (var i = 0; i < 10; i++)
            {
                movies.Add(Mock<IMovie>());
            }

            Extended.Stub(ex => ex.Contents).Return(movies.AsEnumerable());
            Extended.Stub(ex => ex.ListNonViolent()).Return(movies.Take(5));
        }
    }
}