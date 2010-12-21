namespace MavenThought.Commons.Testing.Example
{
    /// <summary>
    /// Critic of movies
    /// </summary>
    public interface IMovieCritic
    {
        /// <summary>
        /// Inidcates wheter a movie is violent
        /// </summary>
        /// <param name="movie">Movie to check</param>
        /// <returns><c>true</c> if the movie is violent, <c>false</c> otherwise</returns>
        bool IsViolent(IMovie movie);
    }
}