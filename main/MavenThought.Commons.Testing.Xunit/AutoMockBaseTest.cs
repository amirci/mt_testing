using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Microsoft.Practices.ServiceLocation;
using Rhino.Mocks;
using StructureMap.AutoMocking;
using Xunit;

namespace MavenThought.Commons.Testing
{
    /// <summary>
    /// Base test with auto mocking of the SUT
    /// </summary>
    /// <typeparam name="TSut">Class to test</typeparam>
    /// <typeparam name="TContract">Type of the contract</typeparam>
    public abstract class AutoMockBaseTest<TSut, TContract>
        : BaseTestWithSut<TContract> where TSut : class, TContract
    {
        /// <summary>
        /// Gets the auto mocker instance
        /// </summary>
        protected RhinoAutoMocker<TSut> AutoMocker { get; private set; }

        /// <summary>
        /// Gets the concrete instance
        /// </summary>
        protected TSut ConcreteSut
        {
            get { return (TSut)this.Sut; }
        }

        /// <summary>
        /// Set the SUT as partial mock
        /// </summary>
        public void PartialMockSut()
        {
            this.AutoMocker.PartialMockTheClassUnderTest();
        }

        /// <summary>
        /// Dependency getter
        /// </summary>
        /// <typeparam name="T">Type of the dependency</typeparam>
        /// <returns>The auto mocker dependency</returns>
        public T Dep<T>() where T : class
        {
            return this.AutoMocker.Get<T>();
        }

        /// <summary>
        /// Create the by returning the auto mocker class under test
        /// </summary>
        /// <returns>The result of obtaining the class under test</returns>
        protected override TContract CreateSut()
        {
            return this.AutoMocker.ClassUnderTest;
        }

        /// <summary>
        /// Create a stub by returning the dependency
        /// </summary>
        /// <typeparam name="TTarget">Target of the stub</typeparam>
        /// <typeparam name="TResult">Type of the dependency</typeparam>
        /// <param name="fn">Function to stub</param>
        protected void Stub<TTarget, TResult>(Function<TTarget, TResult> fn) where TResult : class where TTarget : class
        {
            Dep<TTarget>().Stub(fn).Return(Dep<TResult>());
        }

        /// <summary>
        /// Create the auto mocker before each test
        /// </summary>
        protected override void BeforeEachTest()
        {
            base.BeforeEachTest();

            this.AutoMocker = new RhinoAutoMocker<TSut>(MockMode.AAA);
        }

        /// <summary>
        /// Asserts the constructor has injected the specified value
        /// </summary>
        /// <typeparam name="TResult">Type of the result of the functor</typeparam>
        /// <param name="func">Functor to use to assert the injection</param>
        protected void AssertDependencyInjection<TResult>(Func<TContract, TResult> func)
            where TResult : class
        {
            Assert.Same(Dep<TResult>(), func(this.Sut));
        }

        /// <summary>
        /// Stubs the service locator to return an instance of T
        /// </summary>
        /// <typeparam name="T">Type of the service</typeparam>
        protected void StubService<T>() where T : class
        {
            Stub<IServiceLocator, T>(srv => srv.GetInstance<T>());
        }

        /// <summary>
        /// Raise a property changed for dependency T
        /// </summary>
        /// <typeparam name="T">Type of the dependency</typeparam>
        protected void RaisePropertyChanged<T>(string propertyName) where T : class, INotifyPropertyChanged
        {
            Dep<T>().RaisePropertyChanged(propertyName);
        }

        /// <summary>
        /// Raise a property changed for dependency T
        /// </summary>
        /// <typeparam name="TSource">Type of the source property</typeparam>
        /// <typeparam name="TResult">Type of the result</typeparam>
        protected void RaisePropertyChanged<TSource, TResult>(Expression<Func<TSource, TResult>> expression) where TSource : class, INotifyPropertyChanged
        {
            Dep<TSource>().RaisePropertyChanged(expression);
        }
    }
}