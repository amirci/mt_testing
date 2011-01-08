using MavenThought.Commons.Testing.Example;

namespace MavenThought.Commons.Testing.Tests
{
    /// <summary>
    /// Base specification for SimpleMovieLibrary
    /// </summary>
    public abstract class SimpleMovieLibrarySpecification
        : AutoMockSpecification<SimpleMovieLibrary, IMovieLibrary>
    {
    }
}