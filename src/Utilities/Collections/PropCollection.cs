using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WForest.UI.Props;
using WForest.UI.Props.Actions;

namespace WForest.Utilities.Collections
{
    public class PropCollection: TypeDictionary<Prop>, IEnumerable<Prop>
    {
       public void AddProp(Prop prop) => Add(prop);


        public List<Prop> GetByProp<TP>() where TP : Prop => Get<TP>();

        public bool AnyInteractionProp()
            => Data.ContainsKey(typeof(OnEnter)) || Data.ContainsKey(typeof(OnEnter)) ||
               Data.ContainsKey(typeof(OnPress)) || Data.ContainsKey(typeof(OnRelease));


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