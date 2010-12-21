using System;
using System.Collections.Generic;

namespace MavenThought.Commons.Testing.Example
{
    /// <summary>
    /// Base interface for movie library
    /// </summary>
    public interface IMovieLibrary
    {
        /// <summary>
        /// Notfies when a movies is added
        /// </summary>
        event EventHandler<MediaLibraryArgs> Added;

        /// <summary>
        /// Gets the contents of the library
        /// </summary>
        IEnumerable<IMovie> Contents { get; }

        /// <summary>
        /// Adds a movie to the library
        /// </summary>
        /// <param name="movie">Movie to add</param>
        void Add(IMovie movie);

        /// <summary>
        /// Clears the contents of the library
        /// </summary>
        void Clear();

        /// <summary>
        /// Finds the poster for the movie
        /// </summary>
        /// <param name="movie">Movie to search for</param>
        /// <returns>The name of the poster</returns>
        string Poster(IMovie movie);

        /// <summary>
        /// imports a dictionary of title x release date into the library
        /// </summary>
        /// <param name="movies">Collection to be imported</param>
        void Import(IDictionary<string, DateTime> movies);

        /// <summary>
        /// Lists non violent movies
        /// </summary>
        /// <returns>The collection of movies that are non violent according to the critic</returns>
        IEnumerable<IMovie> ListNonViolent();
    }
}