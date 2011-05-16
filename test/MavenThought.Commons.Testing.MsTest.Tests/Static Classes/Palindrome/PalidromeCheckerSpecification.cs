namespace MavenThought.Commons.Testing.Example.Palindrome
{
    /// <summary>
    /// Base specification for PalidromeChecker
    /// </summary>
    public abstract class PalidromeCheckerSpecification
        : BaseSpecification
    {
        /// <summary>
        /// Gets or sets the actual value obtained
        /// </summary>
        protected bool Actual { get; set; }
    }
}