using Microsoft.Xna.Framework;

namespace WForest.UI.Widgets
{
    public class Container : Widget
    {
        internal Container() : base(Rectangle.Empty){}
        internal Container(Rectangle space) : base(space){}
    }
}