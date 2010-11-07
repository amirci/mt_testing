using System;
using System.Collections.Generic;

namespace MavenThought.Commons.Testing
{
    /// <summary>
    /// AreXXX .... extensions
    /// </summary>
    public partial class Assert
    {
        /// <summary>
        /// Checks that all the elements in the collection are not null
        /// </summary>
        /// <typeparam name="T">Type of the collection</typeparam>
        /// <param name="collection">Collection use</param>
        public static void AreNotNull<T>(IEnumerable<T> collection)
        {
            AssertForEachElement(collection, t => IsNotNull(t));
        }

        /// <summary>
        /// Checks that all the elements returned by the functor are not null
        /// </summary>
        /// <typeparam name="T">Type of the collection</typeparam>
        /// <typeparam name="TResult">Type of the functor result</typeparam>
        /// <param name="collection">Collection to use</param>
        /// <param name="fn">Functor to use</param>
        public static void AreNotNull<T, TResult>(IEnumerable<T> collection, Func<T, TResult> fn)
        {
            AssertForEachElement(collection, t => IsNotNull(fn(t)));
        }

        /// <summary>
        /// Runs an action for each element in the collection
        /// </summary>
        /// <typeparam name="T">Type of the collection</typeparam>
        /// <param name="collection">Collection to use</param>
        /// <param name="action">Action to run</param>
        private static void AssertForEachElement<T>(IEnumerable<T> collection, Action<T> action)
        {
            foreach (var e in collection)
            {
                action(e);
            }
        }
    }
}
