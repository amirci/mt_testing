using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Rhino.Mocks;

namespace MavenThought.Commons.Testing
{
    /// <summary>
    /// Extension for mock objects
    /// </summary>
    public static class NotifyPropertyChangedExtensions
    {
        /// <summary>
        /// Raises the property changed event in the mock object
        /// </summary>
        /// <param name="mock">Mock instance to use</param>
        /// <param name="propertyName">Property name to use in the event</param>
        public static void RaisePropertyChanged(this INotifyPropertyChanged mock, string propertyName)
        {
            mock.Raise(mo => mo.PropertyChanged += null, mock, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the property changed event in the mock object
        /// </summary>
        /// <typeparam name="TSource">Type of the source mock</typeparam>
        /// <typeparam name="TResult">Type of the property result</typeparam>
        /// <param name="mock">Mock instance to use</param>
        /// <param name="property">Property expression to get the name from</param>
        public static void RaisePropertyChanged<TSource, TResult>(this TSource mock, Expression<Func<TSource, TResult>> property)
            where TSource : class, INotifyPropertyChanged
        {
            RaisePropertyChanged(mock, property.GetPropertyName());
        }

        /// <summary>
        /// Asserts that a property has raised the expected property changed
        /// </summary>
        /// <param name="handler">Handler to check</param>
        /// <param name="sender">Source that sends the event</param>
        /// <param name="propertyName">name to check</param>
        public static void AssertPropertyChangedWasCalled(this PropertyChangedEventHandler handler,
                                                          INotifyPropertyChanged sender, string propertyName)
        {
            handler
                .AssertWasCalled(h => h(Arg.Is(sender),
                                        Arg<PropertyChangedEventArgs>
                                            .Matches(args => args.PropertyName == propertyName)));
        }

        /// <summary>
        /// Asserts that a property has raised the expected property changed
        /// </summary>
        /// <typeparam name="TSource">Type of the source that sends the event</typeparam>
        /// <typeparam name="TResult">Type of the result of the property</typeparam>
        /// <param name="handler">Handler to check</param>
        /// <param name="sender">Source that sends the event</param>
        /// <param name="property">Property expression to get the name from</param>
        public static void AssertPropertyChangedWasCalled<TSource, TResult>(this PropertyChangedEventHandler handler,
                                                                            TSource sender,
                                                                            Expression<Func<TSource, TResult>> property)
            where TSource : class, INotifyPropertyChanged
        {
            AssertPropertyChangedWasCalled(handler, sender, property.GetPropertyName());
        }

        /// <summary>
        /// Asserts that a property changed has been raised for a specific property
        /// </summary>
        /// <param name="handler">handler to check</param>
        /// <param name="propertyName">Property to check for</param>
        public static void AssertPropertyChangedWasCalled(this PropertyChangedEventHandler handler, string propertyName)
        {
            handler
                .AssertWasCalled(h => h(Arg<object>.Is.Anything,
                                        Arg<PropertyChangedEventArgs>
                                            .Matches(args => args.PropertyName == propertyName)));
        }

        /// <summary>
        /// Asserts that a property changed has been raised for a specific property
        /// </summary>
        /// <typeparam name="TSource">Type of the source</typeparam>
        /// <typeparam name="TResult">Type of the result</typeparam>
        /// <param name="handler">Handler to check</param>
        /// <param name="property">Property expression to get the name from</param>
        public static void AssertPropertyChangedWasCalled<TSource, TResult>(this PropertyChangedEventHandler handler,
                                                                            Expression<Func<TSource, TResult>> property)
            where TSource : class, INotifyPropertyChanged
        {
            AssertPropertyChangedWasCalled(handler, property.GetPropertyName());
        }

        /// <summary>
        /// Assert that a property changed event has been raised
        /// </summary>
        /// <param name="handler">Handler to check for</param>
        public static void AssertPropertyChangedWasCalled(this PropertyChangedEventHandler handler)
        {
            handler.AssertWasCalled(h => h(Arg<object>.Is.Anything, Arg<PropertyChangedEventArgs>.Is.Anything));
        }

        /// <summary>
        /// Asserts that a property has not raised the expected property changed
        /// </summary>
        /// <param name="handler">Handler to check</param>
        /// <param name="sender">Source that sends the event</param>
        /// <param name="propertyName">name to check</param>
        public static void AssertPropertyChangedWasNotCalled(this PropertyChangedEventHandler handler,
                                                             INotifyPropertyChanged sender, string propertyName)
        {
            handler
                .AssertWasNotCalled(h => h(Arg.Is(sender), Arg<PropertyChangedEventArgs>
                                                               .Matches(args => args.PropertyName == propertyName)));
        }

        /// <summary>
        /// Asserts that a property has not raised the expected property changed
        /// </summary>
        /// <typeparam name="TSource">Type of the source</typeparam>
        /// <typeparam name="TResult">Type of the result</typeparam>
        /// <param name="handler">Handler to check</param>
        /// <param name="sender">Source that sends the event</param>
        /// <param name="property">Property expression to get the name from</param>
        public static void AssertPropertyChangedWasNotCalled<TSource, TResult>(this PropertyChangedEventHandler handler,
                                                                               TSource sender,
                                                                               Expression<Func<TSource, TResult>>
                                                                                   property)
            where TSource : class, INotifyPropertyChanged
        {
            AssertPropertyChangedWasNotCalled(handler, sender, property.GetPropertyName());
        }

        /// <summary>
        /// Asserts that the property changed event was not raised 
        /// </summary>
        /// <param name="handler">Handler to extend</param>
        /// <param name="propertyName">Property to check</param>
        public static void AssertPropertyChangedWasNotCalled(this PropertyChangedEventHandler handler,
                                                             string propertyName)
        {
            handler
                .AssertWasNotCalled(h => h(Arg<object>.Is.Anything,
                                           Arg<PropertyChangedEventArgs>
                                               .Matches(args => args.PropertyName == propertyName)));
        }

        /// <summary>
        /// Asserts that the property changed event was not raised 
        /// </summary>
        /// <typeparam name="TSource">Type of the source</typeparam>
        /// <typeparam name="TResult">Type of the result</typeparam>
        /// <param name="handler">Handler to check</param>
        /// <param name="property">Property expression to get the name from</param>
        public static void AssertPropertyChangedWasNotCalled<TSource, TResult>(this PropertyChangedEventHandler handler,
                                                                               Expression<Func<TSource, TResult>>
                                                                                   property)
            where TSource : class, INotifyPropertyChanged
        {
            AssertPropertyChangedWasNotCalled(handler, property.GetPropertyName());
        }

        /// <summary>
        /// Asserts that the property changed event was not raised 
        /// </summary>
        /// <param name="handler">Handler to extend</param>
        public static void AssertPropertyChangedWasNotCalled(this PropertyChangedEventHandler handler)
        {
            handler.AssertWasNotCalled(h => h(Arg<object>.Is.Anything, Arg<PropertyChangedEventArgs>.Is.Anything));
        }
    }
}