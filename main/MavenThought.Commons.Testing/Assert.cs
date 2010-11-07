using System;
using System.Collections.Generic;
using System.Linq;
using Gallio.Framework.Assertions;

namespace MavenThought.Commons.Testing
{
    /// <summary>
    /// Extension of Assert class
    /// </summary>
    public partial class Assert : MbUnit.Framework.Assert
    {
        /// <summary>
        /// Cheqs if a sequence if subsequence of another
        /// </summary>
        /// <typeparam name="T">Type of the collection</typeparam>
        /// <param name="collection">Collection to check</param>
        /// <param name="subSequence">Subsequence to look for</param>
        public static void Includes<T>(IEnumerable<T> collection, IEnumerable<T> subSequence)
        {
            var result = collection.Intersect(subSequence);

            AreElementsEqual(result, subSequence);
        }

        /// <summary>
        /// Asserts for all the elements in the collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="predicate"></param>
        public new static void ForAll<T>(IEnumerable<T> collection, Predicate<T> predicate)
        {
            var i = 0;

            foreach (var e in collection)
            {
                IsTrue(predicate(e), "The element in position {0} does not match the predicate", i++);
            }
        }

        /// <summary>
        /// Throws an exception if the condition is false
        /// </summary>
        /// <param name="condition">Condition to check</param>
        /// <param name="message">message to use</param>
        /// <param name="args">Arguments for the message</param>
        private static void ThrowIf(bool condition, string message, params object[] args)
        {
            if (condition)
            {
                var builder = new AssertionFailureBuilder(string.Format(message, args));

                throw new AssertionFailureException(builder.ToAssertionFailure(), false);
            }
        }
    }
}
