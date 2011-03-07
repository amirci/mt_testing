using System;

namespace MavenThought.Commons.Testing.Example
{
    /// <summary>
    /// Checker for palindrome words or phrases (u can read them backwards and is the same)
    /// </summary>
    public class PalindromeChecker
    {
        /// <summary>
        /// Checks if a word is palindrome
        /// </summary>
        /// <param name="phrase">Word or phrase to check</param>
        /// <returns><c>true</c> if is a palindrome word, <c>false otherwise</c></returns>
        public static bool IsPalindrome(string phrase)
        {
            var sanitized = phrase
                .Replace("\'", string.Empty)
                .Replace(" ", string.Empty)
                .Replace(":", string.Empty)
                .Replace(";", string.Empty)
                .Replace(",", string.Empty)
                .ToLower();

            var reverse = sanitized.ToCharArray();

            Array.Reverse(reverse);

            var reversed = new string(reverse);

            return sanitized == reversed;
        }
    }
}