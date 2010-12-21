using System;

namespace MavenThought.Commons.Testing.Example
{
    /// <summary>
    /// Arguments to notify
    /// </summary>
    public class MediaLibraryArgs : EventArgs
    {
        public IMovie Movie { get; set; }
    }
}