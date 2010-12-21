using System;

namespace MavenThought.Commons.Testing.Example
{
    /// <summary>
    /// Factory to create movies
    /// </summary>
    public interface IMovieFactory
    {
        /// <summary>
        /// Create a new movie
        /// </summary>
        /// <param name="title">Title of the movie</param>
        /// <param name="release">Release date of the movie</param>
        /// <returns>The movie instance with that title and date</returns>
        IMovie Create(string title, DateTime release);
    }
}