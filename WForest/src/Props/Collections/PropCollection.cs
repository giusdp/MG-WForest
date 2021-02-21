using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WForest.Props.Interfaces;
using WForest.Utilities;

namespace WForest.Props.Collections
{
    /// <summary>
    /// Dictionary of props where the keys are the Types of the props, and the values are lists of props of the same type as the keys. 
    /// </summary>
    public class PropCollection : IPropCollection
    {
        private readonly Dictionary<Type, List<IProp>> _data = new();

        /// <summary>
        /// Check whether the collection has this prop.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Contains<T>() where T : IProp => _data.ContainsKey(typeof(T));


        /// <summary>
        /// Add a prop to the collection. It uses the type of the object as key to a list of props.
        /// It's possible to add multiple props of the same type.
        /// </summary>
        /// <param name="prop"></param>
        public void Add(IProp prop)
        {
            if (prop == null) throw new ArgumentNullException(nameof(prop));
            var type = prop.GetType();
            if (_data.ContainsKey(type)) _data[type].Add(prop);
            else _data[type] = new List<IProp> {prop};
        }

        /// <summary>
        /// Remove the prop given in input from the collection.
        /// </summary>
        /// <param name="prop"></param>
        public void Remove(IProp prop)
        {
            _data.TryGetValue(prop.GetType(), out var list);
            list?.Remove(prop);
        }

        /// <summary>
        /// Get the list of props of type TP Prop. Throws an exception if not found.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> Get<T>() where T : IProp => _data[typeof(T)].Cast<T>();


        /// <summary>
        /// Try to get the list of props of type Prop.
        /// Returns Some with the list if prop type found, None otherwise.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Maybe<IEnumerable<T>> SafeGet<T>() where T : IProp
        {
            var b = _data.TryGetValue(typeof(T), out var l);
            return b ? Maybe.Some(l.Cast<T>()) : Maybe.None;
        }

        #region IEnumerable

        /// <summary>
        /// Get the enumerator of the type dictionary.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<IProp> GetEnumerator()
        {
            var props = _data.Values.SelectMany(l => l);
            foreach (var p in props) yield return p;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}