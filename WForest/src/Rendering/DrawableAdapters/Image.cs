using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RectangleF = WForest.Utilities.RectangleF;

namespace WForest.Rendering.DrawableAdapters
{
    public class Image : Drawable
    {
        public Image(Texture2D texture) : base(texture)
        {
        }


        public override void Draw(IRenderer renderer, RectangleF space, Color color)
        {
           renderer.Draw(this, space, color); 
        }
    }
}