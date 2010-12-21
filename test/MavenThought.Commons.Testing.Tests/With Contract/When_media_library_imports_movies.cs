using System;
using System.Collections.Generic;
using MavenThought.Commons.Testing.Example;
using Rhino.Mocks;
using SharpTestsEx;

namespace MavenThought.Commons.Testing.Tests
{
    /// <summary>
    /// Specification when importing movies
    /// </summary>
    [Specification]
    public class When_media_library_imports_movies : SimpleMovieLibrarySpecification
    {
        private IDictionary<string, DateTime> _movies;
        private ICollection<IMovie> _expected;

        /// <summary>
        /// Setup movies to import
        /// </summary>
        protected override void GivenThat()
        {
            base.GivenThat();

            this._movies = new Dictionary<string, DateTime>
                               {
                                   {"Young Frankenstein", new DateTime(1972)},
                                   {"Spaceballs", new DateTime(1986)},
                                   {"Blazing Saddles", new DateTime(1974)}
                               };

            this._expected = new List<IMovie>();

            foreach (var pair in _movies)
            {
                var m = Mock<IMovie>();

                var localPair = pair;

                Dep<IMovieFactory>().Stub(f => f.Create(localPair.Key, localPair.Value)).Return(m);

                this._expected.Add(m);
            }
        }

        /// <summary>
        /// Import the movies
        /// </summary>
        protected override void WhenIRun()
        {
            this.Sut.Import(_movies);
        }

        /// <summary>
        /// Checks that contain all movies with the same names and dates
        /// </summary>
        [It]
        public void Should_contain_all_the_movies_from_the_dictionary()
        {
            this.Sut.Contents.Should().Have.SameSequenceAs(this._expected);
        }
    }
}