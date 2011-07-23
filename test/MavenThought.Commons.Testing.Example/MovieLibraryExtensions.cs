using System.Collections.Generic;
using System.Linq;

namespace MavenThought.Commons.Testing.Example
{
    /// <summary>
    /// Extend the Movie library to provide useful extensions for statistical purposes
    /// </summary>
    public static class MovieLibraryExtensions
    {
        /// <summary>
        /// Provides a total count of all movies in the library
        /// </summary>
        /// <param name="movieLibrary">Extended Library</param>
        /// <returns>Total Count of all Movies in the Library</returns>
        public static int TotalCount(this IMovieLibrary movieLibrary)
        {
            return movieLibrary.Contents.Count();
        }
        
        /// <summary>
        /// Provides a count of all Non-Violent movies in the library
        /// </summary>
        /// <param name="movieLibrary">Extended Library</param>
        /// <returns>Count of all Non-Violent Movies in the Library</returns>
        public static int NonViolentCount(this IMovieLibrary movieLibrary)
        {
            return movieLibrary.ListNonViolent().Count();
        }

        /// <summary>
        /// Checks the entire library and returns all movies deemed violent by the specified critic.
        /// </summary>
        /// <param name="movieLibrary">Extended Library</param>
        /// <param name="critic">Critic Rating the Movies</param>
        /// <returns>All movies deemed Violent by the Critic</returns>
        public static IEnumerable<IMovie> MoviesRatedViolentByCritic(this IMovieLibrary movieLibrary, IMovieCritic critic)
        {
            return movieLibrary.Contents.Where(critic.IsViolent).AsEnumerable();
        }
    }
}