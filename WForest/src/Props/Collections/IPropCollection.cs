using System.Collections;
using System.Collections.Generic;
using WForest.Props.Interfaces;
using WForest.Utilities;

namespace WForest.Props.Collections
{
    public interface IPropCollection : IEnumerable<IProp>
    {
        void Add(IProp prop);
        void Remove(IProp prop);
        bool Contains<T>() where T : IProp;
        IEnumerable<T> Get<T>() where T : IProp;
        Maybe<IEnumerable<T>> SafeGet<T>() where T : IProp;
    }
}   