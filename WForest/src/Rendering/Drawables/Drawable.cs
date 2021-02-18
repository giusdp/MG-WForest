using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RectangleF = WForest.Utilities.RectangleF;

namespace WForest.Rendering.Drawables
{
    /// <summary>
    /// Abstraction for drawable objects (Texture2D, NinePatch etc.) to be used by widgets.
    /// </summary>
    public abstract class Drawable
    {
        protected Texture2D Texture;
        public int Width { get; }
        public int Height { get; }

        public Color? TintColor { get; set; }

        protected Drawable(Texture2D texture, Color? tintColor = null)
        {
            Texture = texture;
            TintColor = tintColor;
            Width = texture.Width;
            Height = texture.Height;
        }

        public abstract void Draw(IRenderer renderer, RectangleF space, Color color);

        public static implicit operator Texture2D(Drawable da) => da.Texture;

        protected static Color MultiplyColor(Color first, Color second)
        {
            return new()
            {
                R = (byte) (first.R * second.R / 255),
                G = (byte) (first.G * second.G / 255),
                B = (byte) (first.B * second.B / 255),
                A = (byte) (first.A * second.A / 255)
            };
        }
    }
}