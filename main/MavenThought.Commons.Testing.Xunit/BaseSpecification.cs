using System;
using Xunit;

namespace MavenThought.Commons.Testing
{
    /// <summary>
    /// Base specification class using BDD like style
    /// </summary>
    /// <remarks>
    /// The tests should be written overriding
    /// <list>
    ///   <item>GivenThat</item>
    ///   <item>WhenIRun</item>
    ///   <item>AndThenAfterEverything</item>
    /// </list>
    /// </remarks>
    public abstract class BaseSpecification
        : BaseTest
    {
        /// <summary>
        /// Gets or sets the exception thrown while running
        /// </summary>
        protected Exception ExceptionThrown { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is a creations specification
        /// </summary>
        protected bool IsCreation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the exception when running should be caught
        /// </summary>
        protected bool CatchExceptionOnRunning { get; set; }

        /// <summary>
        /// Initializes an instance of <see cref="BaseSpecification"/>
        /// </summary>
        protected BaseSpecification()
        {
            this.CatchExceptionOnRunning = this.HasAttribute<ExceptionSpecificationAttribute>();

            this.IsCreation = this.HasAttribute<ConstructorSpecificationAttribute>();
        }

        /// <summary>
        /// Asserts an exception has been thrown
        /// </summary>
        protected void AssertExceptionThrown()
        {
            AssertExceptionThrown<Exception>();
        }

        /// <summary>
        /// Asserts an exception of Type T has been thrown
        /// </summary>
        /// <typeparam name="T">Type of the exception</typeparam>
        protected void AssertExceptionThrown<T>() where T : Exception
        {
            Assert.IsAssignableFrom<T>(this.ExceptionThrown);
        }

        /// <summary>
        /// Call GivenThatBeforeAnything
        /// </summary>
        protected override void BeforeAllTests()
        {
            this.GivenThatBeforeAnything();
        }

        /// <summary>
        /// Calls create context
        /// </summary>
        protected override void BeforeEachTest()
        {
            this.GivenThat();

            if (this.IsCreation)
            {
                return;
            }

            Action whenIRun = this.WhenIRun;

            if (this.CatchExceptionOnRunning)
            {
                whenIRun = () => this.RegisterException(this.WhenIRun);
            }

            whenIRun();
        }

        /// <summary>
        /// Call cleanup after all tests
        /// </summary>
        protected override void AfterEachTest()
        {
            this.AndThenAfterEverything();
        }

        /// <summary>
        /// Placeholder to specify context globally
        /// </summary>
        protected void GivenThatBeforeAnything()
        {
        }

        /// <summary>
        /// Pleacholder to specify context 
        /// </summary>
        protected virtual void GivenThat()
        {
        }

        /// <summary>
        /// Method to run to apply the expected functionality
        /// </summary>
        protected virtual void WhenIRun()
        {
            throw new NotImplementedException("Please implement WhenIRun in the derived class or use ConstructorSpecification attrubute in the class");
        }

        /// <summary>
        /// Place holder to clean up after each behavior/fact
        /// </summary>
        protected virtual void AndThenAfterEverything()
        {
        }

        /// <summary>
        /// Runs an action registering the exception in the ExceptionThrown property
        /// </summary>
        /// <param name="action">Action to run</param>
        protected void RegisterException(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                this.ExceptionThrown = e;
            }
        }

        /// <summary>
        /// Checks if the type has the attribute
        /// </summary>
        /// <typeparam name="T">Type of the attribute</typeparam>
        /// <returns>True if the type has it, false otherwise</returns>
        protected bool HasAttribute<T>() where T : Attribute
        {
            var attributes = this.GetType().GetCustomAttributes(typeof(T), true);

            return attributes.Length > 0;
        }
    }
}