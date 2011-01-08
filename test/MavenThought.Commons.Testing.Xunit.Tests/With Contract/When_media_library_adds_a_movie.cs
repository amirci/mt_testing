using System;
using MavenThought.Commons.Testing.Example;
using Rhino.Mocks;
using SharpTestsEx;

namespace MavenThought.Commons.Testing.Tests
{
    /// <summary>
    /// Specification when adding a movie
    /// </summary>
    [Specification]
    public class When_media_library_adds_a_movie : SimpleMovieLibrarySpecification
    {
        private IMovie _movie;
        private EventHandler<MediaLibraryArgs> _handler;

        /// <summary>
        /// Setup the movie
        /// </summary>
        protected override void GivenThat()
        {
            base.GivenThat();

            this._movie = Mock<IMovie>();

            this._handler = Mock<EventHandler<MediaLibraryArgs>>();
        }

        /// <summary>
        /// Register the handler
        /// </summary>
        protected override void AndGivenThatAfterCreated()
        {
            base.AndGivenThatAfterCreated();

            this.Sut.Added += this._handler;
        }

        /// <summary>
        /// Add the movie
        /// </summary>
        protected override void WhenIRun()
        {
            this.Sut.Add(_movie);
        }

        /// <summary>
        /// Checks that movie has been added
        /// </summary>
        [It]
        public void Should_add_the_movie_to_the_library()
        {
            this.Sut.Contents.Should().Have.SameSequenceAs(this._movie);
        }

        /// <summary>
        /// Checks that notifies the addition
        /// </summary>
        [It]
        public void Should_notify_the_movie_was_added()
        {
                this._handler.AssertWasCalled(h => h(Arg.Is(this.Sut),
                                                     Arg<MediaLibraryArgs>
                                                     .Matches(arg => arg.Movie == this._movie)));
        }
    }
}