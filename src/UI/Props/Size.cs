using Microsoft.Xna.Framework;

namespace PiBa.UI.Props
{
    public class Size : Prop<Point>
    {
        public sealed override Point Value { get; set; }

        public Size(Point value)
        {
            Value = value;
        }
    }
}