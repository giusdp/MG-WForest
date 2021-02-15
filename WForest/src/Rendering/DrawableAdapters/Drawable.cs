using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RectangleF = WForest.Utilities.RectangleF;

namespace WForest.Rendering.DrawableAdapters
{
    /// <summary>
    /// Abstraction for drawable objects (Texture2D, NinePatch etc.) to be used by widgets.
    /// </summary>
    public abstract class Drawable
    {
        protected Texture2D Texture;
        public int Width { get; }
        public int Height { get; }

        protected Drawable(Texture2D texture)
        {
            Texture = texture;
            Width = texture.Width;
            Height = texture.Height;
        }

        public abstract void Draw(IRenderer renderer, RectangleF space, Color color);

        public static implicit operator Texture2D(Drawable da) => da.Texture;
    }
}