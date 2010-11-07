using System;

namespace MavenThought.Commons.Testing
{
    /// <summary>
    /// Redefinition of Factory attribute
    /// </summary>
    public class FactoryAttribute : MbUnit.Framework.FactoryAttribute
    {
        public FactoryAttribute(string memberName) : base(memberName)
        {
        }

        public FactoryAttribute(Type type, string memberName) : base(type, memberName)
        {
        }
    }
}