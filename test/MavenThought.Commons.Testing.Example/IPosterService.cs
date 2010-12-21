namespace MavenThought.Commons.Testing.Example
{
    /// <summary>
    /// Service to find posters for movies
    /// </summary>
    public interface IPosterService
    {
        /// <summary>
        /// Finds the poster for a movie
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        string FindPoster(IMovie movie);
    }
}