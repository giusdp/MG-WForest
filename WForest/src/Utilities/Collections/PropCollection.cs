using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WForest.UI.Props.Interfaces;

namespace WForest.Utilities.Collections
{
    /// <summary>
    /// Dictionary of props where the keys are the Types of the props, and the values are lists of props of the same type as the keys. 
    /// </summary>
    public class PropCollection : TypeDictionary<IProp>, IEnumerable<IProp>
    {
        /// <summary>
        /// Add a prop to the collection. It uses the type of the object as key to a list of props.
        /// It's possible to add multiple props of the same type.
        /// </summary>
        /// <param name="prop"></param>
        public void AddProp(IProp prop) => Add(prop);

        public void RemoveSingleProp(IProp prop)
        {
            Data.TryGetValue(prop.GetType(), out var list);
            list?.Remove(prop);
        }
        
        public bool Contains<TP>() where TP : IProp => Data.ContainsKey(typeof(TP));

        /// <summary>
        /// Try to get the list of props of type Prop.
        /// Returns Some with the list if prop type found, None otherwise.
        /// </summary>
        /// <typeparam name="TP"></typeparam>
        /// <returns></returns>
        public Maybe<List<IProp>> SafeGetByProp<TP>() where TP : IProp
        {
            var b = Data.TryGetValue(typeof(TP), out var l);
            return b ? Maybe.Some(l!) : Maybe.None;
        }

        /// <summary>
        /// Get the list of props of type TP Prop. Throws an exception if not found.
        /// </summary>
        /// <typeparam name="TP"></typeparam>
        /// <returns></returns>
        public List<IProp> GetByProp<TP>() where TP : IProp => Get<TP>();

        /// <summary>
        /// Get the enumerator of the type dictionary.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<IProp> GetEnumerator()
        {
            var props = Data.Values.SelectMany(l => l);
            foreach (var p in props) yield return p;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}