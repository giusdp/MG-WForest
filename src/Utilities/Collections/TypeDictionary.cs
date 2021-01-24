using System;
using System.Collections.Generic;

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
}