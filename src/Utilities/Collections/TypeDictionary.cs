using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WForest.UI.Props;

namespace WForest.Utilities.Collections
{
    public class TypeDictionary<T>
    {
        protected readonly Dictionary<Type, List<T>> Data = new Dictionary<Type, List<T>>();

        protected void Add(T element)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));
            var type = element.GetType();
            if (Data.ContainsKey(type)) Data[type].Add(element);
            else Data[type] = new List<T> {element};
            Console.WriteLine("added : " + type);
        }

        protected List<T> Get<TP>() where TP : T => Data[typeof(TP)];
    }

    public class PropCollection : TypeDictionary<Prop>, IEnumerable<Prop>
    {
        public void AddProp(Prop prop) => Add(prop);


        public List<Prop> GetByProp<TP>() where TP : Prop => Get<TP>();

        public IEnumerator<Prop> GetEnumerator()
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