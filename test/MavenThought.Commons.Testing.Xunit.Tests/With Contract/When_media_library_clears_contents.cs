using MavenThought.Commons.Testing.Example;
using SharpTestsEx;

namespace MavenThought.Commons.Testing.Tests
{
    /// <summary>
    /// Specification when clearing the contents
    /// </summary>
    [Specification]
    public class When_media_library_clears_contents : SimpleMovieLibrarySpecification
    {
        /// <summary>
        /// Setup the movies
        /// </summary>
        protected override void AndGivenThatAfterCreated()
        {
            base.AndGivenThatAfterCreated();

            for (var i = 0; i < 10; i++)
            {
                this.Sut.Add(Mock<IMovie>());       
            }
        }

        /// <summary>
        /// Clear the contents
        /// </summary>
        protected override void WhenIRun()
        {
            this.Sut.Clear();
        }

        /// <summary>
        /// Checks that the contents are empty
        /// </summary>
        [It]
        public void Should_clear_the_contents()
        {
            this.Sut.Contents.Should().Be.Empty();
        }
    }
}