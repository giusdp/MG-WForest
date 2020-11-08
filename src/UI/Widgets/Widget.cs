using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PiBa.UI.Widgets
{
    public class Widget
    {
        public Rectangle Space { get; set; }

        public Widget(Rectangle space)
        {
            Space = space;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        public virtual bool IsHovered(Point mouseLocation)
        {
            var (x, y) = mouseLocation;
            var isInsideHorizontally = x >= Space.X && x <= Space.X + Space.Width - 1;
            var isInsideVertically = y >= Space.Y && y <= Space.Y + Space.Height - 1;
            return isInsideHorizontally && isInsideVertically;
        }
        
        public override string ToString() => $"Basic Widget with a Space of {Space}";
    }
}