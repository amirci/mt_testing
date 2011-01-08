using System;
using Xunit;

namespace MavenThought.Commons.Testing
{
    /// <summary>
    /// Attribute to ignore specifications
    /// </summary>
    [Obsolete("xUnit.NET uses a Skip=\"reason\" syntax to ignore tests b/c it requires a reason if you are deciding to ignore a test.", true)]
    public class IgnoreAttribute : FactAttribute
    {
    }
}
