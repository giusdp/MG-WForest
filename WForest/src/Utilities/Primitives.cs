using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WForest.Rendering;

namespace WForest.Utilities
{
    /// <summary>
    /// Static class with helper methods to create texture and draw borders.
    /// </summary>
    public static class Primitives
    {
        private static Texture2D? _blankTexture;

        /// <summary>
        /// Spritebatch extension method to create a colored 1x1 Texture2D, which can be used
        /// to draw shapes or borders. 
        /// </summary>
        /// <param name="s">The spritebatch</param>
        /// <param name="color">The color to use with the create texture.</param>
        /// <returns>A blank colored 1x1 Texture2D</returns>
        public static Texture2D CreateTexture(this IRenderer s, Color color)
        {
            var blankTexture = new Texture2D(s.GraphicsDevice, 1, 1);
            blankTexture.SetData(new[] {color});
            return blankTexture;
        }

        /// <summary>
        /// Draw a colored rectangle border. 
        /// </summary>
        /// <param name="renderer">Spritebatch to use for drawing.</param>
        /// <param name="rect">The rectangle outline.</param>
        /// <param name="color">The color of the border.</param>
        /// <param name="lineWidth">The width of the border.</param>
        public static void DrawBorder(IRenderer renderer, Rectangle rect, Color color, int lineWidth)
        {
            _blankTexture ??= renderer.CreateTexture(Color.White);
            var x = rect.X;
            var y = rect.Y;
            var width = rect.Width;
            var height = rect.Height;
            renderer.Draw(_blankTexture, new Rectangle(x, y, lineWidth, height + lineWidth), color);
            renderer.Draw(_blankTexture, new Rectangle(x, y, width + lineWidth, lineWidth), color);
            renderer.Draw(_blankTexture, new Rectangle(x + width, y, lineWidth, height + lineWidth), color);
            renderer.Draw(_blankTexture, new Rectangle(x, y + height, width + lineWidth, lineWidth), color);
        }
    }
}