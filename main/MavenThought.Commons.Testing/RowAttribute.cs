namespace MavenThought.Commons.Testing
{
    /// <summary>
    /// Redefine Row attribute to use it as Row
    /// </summary>
    public class RowAttribute : MbUnit.Framework.RowAttribute
    {
        public RowAttribute(params object[] values) 
            : base(values)
        {
        }
    }
}