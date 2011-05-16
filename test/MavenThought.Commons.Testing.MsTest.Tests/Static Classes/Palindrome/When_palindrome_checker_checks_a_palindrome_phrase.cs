using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;

namespace MavenThought.Commons.Testing.Example.Palindrome
{
    /// <summary>
    /// Specification when checking palindrome phrases
    /// </summary>
    [TestClass]
    public class When_palindrome_checker_checks_a_palindrome_phrase
        : PalidromeCheckerSpecification
    {
        /// <summary>
        /// Phrase to use
        /// </summary>
        private string _phrase;

        protected override void GivenThat()
        {
            base.GivenThat();

            this._phrase = PalindromeFactory().ToList()[0];
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
        [TestMethod]
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