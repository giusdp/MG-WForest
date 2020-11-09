using Microsoft.Xna.Framework;

namespace PiBa.UI.Widgets
{
    public class Container : Widget
    {
        public Container(Rectangle space) : base(space)
        {
        }

        public override string ToString() => $"Container Widget with a Space of {Space}";
    }
}