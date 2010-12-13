using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;

namespace MavenThought.Commons.Testing.MsTest.Tests
{
    /// <summary>
    /// Specification when adding two numbers
    /// </summary>
    [TestClass]
    public class When_calculator_adds_two_numbers : CalculatorSpecification
    {
        private double _arg1;
        private double _arg2;
        private double _actual;

        /// <summary>
        /// Checks that addition works
        /// </summary>
        [TestMethod]
        public void Should_add_both_values()
        {
            _actual.Should().Be(_arg1 + _arg2);
        }

        /// <summary>
        /// Setup the numbers
        /// </summary>
        protected override void GivenThat()
        {
            base.GivenThat();

            _arg1 = 3;
            _arg2 = 5;
        }

        /// <summary>
        /// Add two numbers
        /// </summary>
        protected override void WhenIRun()
        {
            _actual = this.Sut.Add(_arg1, _arg2);
        }
    }
}