using MbUnit.Framework;
using SharpTestsEx;

namespace MavenThought.Commons.Testing.Example.Palindrome
{
    /// <summary>
    /// Specification when the phrase is not palindrome
    /// </summary>
    [Specification]
    public class When_palindrome_checker_checks_a_non_palindrome_phrase 
        : PalidromeCheckerSpecification
    {
        private readonly string _phrase;

        public When_palindrome_checker_checks_a_non_palindrome_phrase(
            [RandomStrings(Count=10, Pattern=@"Weird [A-Z]{5,8} [0-9]{2}")]string phrase)
        {
            _phrase = phrase;
        }

        /// <summary>
        /// Check the value is palindrome
        /// </summary>
        protected override void WhenIRun()
        {
            this.Actual = PalindromeChecker.IsPalindrome(this._phrase);
        }

        /// <summary>
        /// Checks that the result is false
        /// </summary>
        [It]
        public void Should_not_validate_the_phrase_as_palindrome()
        {
            this.Actual.Should().Be.False();
        }
    }
}