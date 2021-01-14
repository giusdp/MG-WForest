using Microsoft.Xna.Framework;

namespace WForest.UI.Widgets
{
    /// <summary>
    /// Widget that acts as a container for other widgets. By itself it does nothing, use it only as a
    /// parent for other widget, giving it a Row or Column property.
    /// </summary>
    public class Container : Widget
    {
        internal Container(Rectangle space) : base(space){}
    }
}