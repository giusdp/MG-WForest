using System;

namespace WForest.Utilities
{
    
    /// <summary>
    /// A container that may or may not contain some value.
    /// It can be two types: Some and None.
    ///
    /// Use it to handle null cases, so you can pattern match against it to check if
    /// there is a value or not.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Maybe<T>
    {
        private Maybe()
        {
        }

        /// <summary>
        /// Represents the presence of data of type T.
        /// </summary>
        public sealed class Some : Maybe<T>
        {
            /// <summary>
            /// Create a Maybe Some object containing some data.
            /// </summary>
            /// <param name="value"></param>
            public Some(T value) => Value = value;
            /// <summary>
            /// The data contained in the Some case of Maybe.
            /// </summary>
            public T Value { get; }
        }

        /// <summary>
        /// Represents the absence of data.
        /// </summary>
        public sealed class None : Maybe<T>
        {
        }

        /// <summary>
        /// Operator to make assignments easier. Lets you pass a T value to a Maybe variable.
        /// <example><code>Maybe<int/> m = 4;</code></example>
        /// Will result in m having a Some with the value 4;
        /// </summary>
        /// <param name="value"></param>
        /// <returns>A Maybe None</returns>
        public static implicit operator Maybe<T>(T value)
        {
            if (value == null)
                return new None();

            return new Some(value);
        }

        /// <summary>
        /// Operator to make assignments easier. Lets you pass a Maybe.None to any Maybe variable.
        /// <example><code>Maybe int m = Maybe.None;</code></example>
        /// </summary>
        /// <param name="value"></param>
        /// <returns>A Maybe None</returns>
        public static implicit operator Maybe<T>(Maybe.MaybeNone value)
        {
            return new None();
        }

        /// <summary>
        /// Try to get the value contained. If it is Some, the value contained is placed in the
        /// out variable and returns true. Otherwise returns false and a null is placed in the variable.
        /// </summary>
        /// <param name="value">Variable in which the data contained in Maybe is placed.</param>
        /// <returns>true if it was Some (contains a value), false if it was None (does not contain any value)</returns>
        public bool TryGetValue(out T value)
        {
            if (this is Some some)
            {
                value = some.Value;
                return true;
            }

            value = default(T)!;
            return false;
        }
    }

    /// <summary>
    /// Static class to create Maybe objects.
    /// </summary>
    public static class Maybe
    {
        /// <summary>
        /// Represents the None case for Maybe.
        /// </summary>
        public class MaybeNone{}

        /// <summary>
        /// Creates a Maybe.None representing the absence of data.
        /// </summary>
        public static MaybeNone None { get; } = new MaybeNone();

        /// <summary>
        /// Creates a Maybe.Some of type T with a value inside.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>A Maybe containing some data.</returns>
        /// <exception cref="ArgumentNullException">The argument cannot be null. Maybe.Some must represents the presence of data.</exception>
        public static Maybe<T> Some<T>(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return new Maybe<T>.Some(value);
        }
    }
}