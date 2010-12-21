using System;
using System.Collections.Generic;
using System.Linq;

namespace MavenThought.Commons.Testing.Example
{
    public class SimpleMovieLibrary : IMovieLibrary
    {
        /// <summary>
        /// Collection to store the movies
        /// </summary>
        private readonly ICollection<IMovie> _contents = new List<IMovie>() ;

        /// <summary>
        /// Service to find movie posters
        /// </summary>
        private readonly IPosterService _posterService;
        /// <summary>
        /// Factory to import movies
        /// </summary>
        private readonly IMovieFactory _factory;

        /// <summary>
        /// Critic to list movies
        /// </summary>
        private readonly IMovieCritic _critic;

        /// <summary>
        /// Initializes a new instances of <see cref="SimpleMovieLibrary"/> class.
        /// </summary>
        /// <param name="posterService">Service to find the poster</param>
        /// <param name="factory">Factory to import the movies</param>
        /// <param name="critic">Critic to filter movies</param>
        public SimpleMovieLibrary(IPosterService posterService, IMovieFactory factory, IMovieCritic critic)
        {
            _posterService = posterService;
            _critic = critic;
            _factory = factory;
        }

        /// <summary>
        /// Notfies when a movies is added
        /// </summary>
        public event EventHandler<MediaLibraryArgs> Added = delegate { };

        /// <summary>
        /// Gets the contents of the library
        /// </summary>
        public IEnumerable<IMovie> Contents
        {
            get { return _contents; }
        }

        /// <summary>
        /// Adds a movie to the library
        /// </summary>
        /// <param name="movie">Movie to add</param>
        public void Add(IMovie movie)
        {
            this._contents.Add(movie);

            this.Added(this, new MediaLibraryArgs { Movie = movie });
        }

        /// <summary>
        /// Clears the contents of the library
        /// </summary>
        public void Clear()
        {
            this._contents.Clear();
        }

        /// <summary>
        /// Finds the poster for the movie
        /// </summary>
        /// <param name="movie">Movie to search for</param>
        /// <returns>The name of the poster</returns>
        public string Poster(IMovie movie)
        {
            return this._posterService.FindPoster(movie);
        }

        /// <summary>
        /// imports a dictionary of title x release date into the library
        /// </summary>
        /// <param name="movies">Collection to be imported</param>
        public void Import(IDictionary<string, DateTime> movies)
        {
            foreach (var pair in movies)
            {
                this._contents.Add(this._factory.Create(pair.Key, pair.Value));
            }
        }

        /// <summary>
        /// Lists non violent movies
        /// </summary>
        /// <returns>The collection of movies that are non violent according to the critic</returns>
        public IEnumerable<IMovie> ListNonViolent()
        {
            return this.Contents
                .Where(movie => !this._critic.IsViolent(movie));
        }
    }
}