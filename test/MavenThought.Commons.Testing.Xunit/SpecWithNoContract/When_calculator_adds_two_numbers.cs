using Xunit;
using SharpTestsEx;

namespace MavenThought.Commons.Testing.NUnit.Tests.SpecWithNoContract
{
    /// <summary>
    /// Specification when adding two numbers
    /// </summary>
    [Specification]
    public class When_calculator_adds_two_numbers : CalculatorSpecification
    {
        /// <summary>
        /// Actual value obtained
        /// </summary>
        private double _actual;

        /// <summary>
        /// Checks that the addition works
        /// </summary>
        [Fact]
        public void Should_add_both_numbers()
        {
            this._actual.Should().Be(5 + 8);
        }

        /// <summary>
        /// Add two numbers
        /// </summary>
        protected override void WhenIRun()
        {
            _actual = this.Sut.Add(5, 8);
        }
    }
}