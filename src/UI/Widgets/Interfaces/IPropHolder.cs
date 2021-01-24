using WForest.UI.Props;
using WForest.Utilities.Collections;

namespace WForest.UI.Widgets.Interfaces
{
    public interface IPropHolder
    {
        PropCollection Props { get; }

        IPropHolder WithProp(Prop prop)
        {
            Props.AddProp(prop);
            return this;
        }
    }
}