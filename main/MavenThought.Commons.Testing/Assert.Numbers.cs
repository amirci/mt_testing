using System.Collections.Generic;

namespace MavenThought.Commons.Testing
{
    public partial class Assert
    {
        /// <summary>
        /// Assert all are not NaN
        /// </summary>
        /// <param name="collection">collection to use</param>
        public static void AreNotNaN(IEnumerable<double> collection)
        {
            AssertForEachElement(collection, IsNotNaN);
        }

        /// <summary>
        /// Assert all are not NaN
        /// </summary>
        /// <param name="collection">collection to use</param>
        public static void AreNotNaN(IEnumerable<float> collection)
        {
            AssertForEachElement(collection, IsNotNaN);
        }

        /// <summary>
        /// Asserts the number is not NaN
        /// </summary>
        /// <param name="num">Number to check</param>
        public static void IsNotNaN(double num)
        {
            ThrowIf(double.IsNaN(num), "Expected a number and got NaN");
        }

        /// <summary>
        /// Asserts the number is not NaN
        /// </summary>
        /// <param name="num">Number to check</param>
        public static void IsNotNaN(float num)
        {
            ThrowIf(double.IsNaN(num), "Expected a number and got NaN");
        }

        /// <summary>
        /// Asserts the number is NaN
        /// </summary>
        /// <param name="num">Number to check</param>
        public static void IsNaN(float num)
        {
            ThrowIf(!double.IsNaN(num), "Expected a NaN and got {0}", num);
        }

        /// <summary>
        /// Asserts the number is NaN
        /// </summary>
        /// <param name="num">Number to check</param>
        public static void IsNaN(double num)
        {
            ThrowIf(!double.IsNaN(num), "Expected a NaN and got {0}", num);
        }
    }
}
