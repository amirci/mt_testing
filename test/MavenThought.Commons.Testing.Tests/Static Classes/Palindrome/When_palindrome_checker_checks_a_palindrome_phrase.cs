using System.Collections.Generic;
using SharpTestsEx;

namespace MavenThought.Commons.Testing.Example.Palindrome
{
    /// <summary>
    /// Specification when checking palindrome phrases
    /// </summary>
    [Specification]
    public class When_palindrome_checker_checks_a_palindrome_phrase
        : PalidromeCheckerSpecification
    {
        /// <summary>
        /// Phrase to use
        /// </summary>
        private readonly string _phrase;

        [Factory("PalindromeFactory")]
        public When_palindrome_checker_checks_a_palindrome_phrase(string phrase)
        {
            _phrase = phrase;
        }

        /// <summary>
        /// Run the check
        /// </summary>
        protected override void WhenIRun()
        {
            this.Actual = PalindromeChecker.IsPalindrome(this._phrase);
        }

        /// <summary>
        /// Checks that the result is palindrome
        /// </summary>
        [It]
        public void Should_check_the_word_as_palindrome()
        {
            this.Actual.Should().Be.True();
        }

        /// <summary>
        /// Factory for palindrome phrases
        /// </summary>
        /// <returns></returns>
        protected static IEnumerable<string> PalindromeFactory()
        {
            yield return "Madam I'm adam";
            yield return "Draw pupil's lip upward";
            yield return "Gateman sees name, garageman sees name tag";
            yield return "Go hang a salami; I'm a lasagna hog";
            yield return "I roamed under it as a tired, nude Maori";
            yield return "Live not on evil";
        }
    }
}