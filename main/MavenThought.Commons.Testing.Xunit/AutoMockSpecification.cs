using System;
using Xunit;

namespace MavenThought.Commons.Testing
{
    /// <summary>
    /// Specification that uses auto mocking.
    /// </summary>
    /// <typeparam name="TSut">Type of the system under test</typeparam>
    /// <typeparam name="TContract">Contract of TSUT</typeparam>
    public abstract class AutoMockSpecification<TSut, TContract>
        : AutoMockBaseTest<TSut, TContract> where TSut : class, TContract
    {
        /// <summary>
        /// Gets or sets the exception thrown while running
        /// </summary>
        protected Exception ExceptionThrown { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the specification is for object creation
        /// </summary>
        protected bool IsCreation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the exception when running should be caught
        /// </summary>
        protected bool CatchExceptionOnRunning { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMockSpecification{TSUT,TContract}"/> class. 
        /// </summary>
        /// <remarks>
        /// Checks if the class has the <see cref="ExceptionSpecificationAttribute"/> declared in order to set
        /// the <see cref="CatchExceptionOnRunning"/> property to true.
        /// Also checks if the class has the <see cref="ConstructorSpecificationAttribute"/> declared in
        /// order to set the <see cref="IsCreation"/> property to true.
        /// </remarks>
        protected AutoMockSpecification()
        {
            this.CatchExceptionOnRunning = this.HasAttribute<ExceptionSpecificationAttribute>();

            this.IsCreation = this.HasAttribute<ConstructorSpecificationAttribute>();
        }

        /// <summary>
        /// Calls create context
        /// </summary>
        protected override void BeforeCreateSut()
        {
            this.GivenThat();
        }

        /// <summary>
        /// Calls because
        /// </summary>
        protected override void AfterCreateSut()
        {
            this.AndGivenThatAfterCreated();

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
            this.AndThenCleanUp();
        }

        /// <summary>
        /// Pleacholder to specify context before Sut is created
        /// </summary>
        protected virtual void GivenThat()
        {
        }

        /// <summary>
        /// Placeholder to specify context after Sut is created
        /// </summary>
        protected virtual void AndGivenThatAfterCreated()
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
        protected virtual void AndThenCleanUp()
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
        /// Asserts an exception has been thrown
        /// </summary>
        protected void AssertExceptionThrown()
        {
            AssertExceptionThrown<Exception>();
        }

        /// <summary>
        /// Asserts an exception of specific type has been thrown
        /// </summary>
        /// <typeparam name="T">Type of the exception</typeparam>
        protected void AssertExceptionThrown<T>() where T : Exception
        {
            Assert.IsAssignableFrom<T>(this.ExceptionThrown);
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