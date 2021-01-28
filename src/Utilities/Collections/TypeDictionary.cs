using System;
using System.Collections.Generic;

namespace WForest.Utilities.Collections
{
    /// <summary>
    /// Abstract dictionary that uses T types as keys and list of T types as values.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class TypeDictionary<T>
    {
        /// <summary>
        /// The Type, List dictionary.
        /// </summary>
        protected readonly Dictionary<Type, List<T>> Data = new Dictionary<Type, List<T>>();

        /// <summary>
        /// Add a T object to the list of the T type key. If it does not exist, it adds T key to the dictionary
        /// with a new List with the element.
        /// </summary>
        /// <param name="element"></param>
        /// <exception cref="ArgumentNullException"></exception>
        protected void Add(T element)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));
            var type = element.GetType();
            if (Data.ContainsKey(type)) Data[type].Add(element);
            else Data[type] = new List<T> {element};
        }

        /// <summary>
        /// Try to get the value of the TP type key. Returns true if it exists and the value is set in tList parameter.
        /// Return false if it does not exist and the tList parameter is set to null.
        /// </summary>
        /// <param name="tList"></param>
        /// <typeparam name="TP"></typeparam>
        /// <returns></returns>
        protected bool TryGetValue<TP>(out List<T>? tList) where TP : T => Data.TryGetValue(typeof(TP), out tList);

        /// <summary>
        /// Get the value of the TP type key. Throws an exception if the key is not found.
        /// </summary>
        /// <typeparam name="TP"></typeparam>
        /// <returns></returns>
        protected List<T> Get<TP>() where TP : T => Data[typeof(TP)];
    }
}