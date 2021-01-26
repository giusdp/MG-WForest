using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WForest.UI.Props.Interfaces;

namespace WForest.Utilities.Collections
{
    public class PropCollection : TypeDictionary<IProp>, IEnumerable<IProp>
    {
        public void AddProp(IProp prop) => Add(prop);
        
        public bool TryGetPropValue<TP>(out List<IProp>? tList) where TP : IProp => Data.TryGetValue(typeof(TP), out tList);
        public List<IProp> GetByProp<TP>() where TP : IProp => Get<TP>();

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