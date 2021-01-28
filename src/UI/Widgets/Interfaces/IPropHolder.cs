using WForest.UI.Props;
using WForest.UI.Props.Interfaces;
using WForest.Utilities.Collections;

namespace WForest.UI.Widgets.Interfaces
{
    /// <summary>
    /// Interface for props handling. It adds a prop collection and a method to add props.
    /// </summary>
    public interface IPropHolder
    {
        /// <summary>
        /// Dictionary of props.
        /// </summary>
        PropCollection Props { get; }

        /// <summary>
        /// Add a prop to the holder and return the updated holder so this method can be chained.
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        IPropHolder WithProp(IProp prop)
        {
            Props.AddProp(prop);
            return this;
        }
    }
}