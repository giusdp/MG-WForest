using WForest.UI.Props;
using WForest.UI.Props.Interfaces;
using WForest.Utilities.Collections;

namespace WForest.UI.Widgets.Interfaces
{
    public interface IPropHolder
    {
        PropCollection Props { get; }

        IPropHolder WithProp(IProp prop)
        {
            Props.AddProp(prop);
            return this;
        }
    }
}