using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RectangleF = WForest.Utilities.RectangleF;

namespace WForest.Rendering.DrawableAdapters
{
    public class Image : Drawable
    {
        public Image(Texture2D texture, Color? tintColor = null) : base(texture, tintColor)
        {
        }


        public override void Draw(IRenderer renderer, RectangleF space, Color color)
        {
            if (TintColor is not null) color = MultiplyColor(color, TintColor.Value);
           renderer.Draw(this, space, color); 
        }
    }
}