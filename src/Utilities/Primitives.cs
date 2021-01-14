using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        public static Texture2D CreateTexture(this SpriteBatch s, Color color)
        {
            var blankTexture = new Texture2D(s.GraphicsDevice, 1, 1);
            blankTexture.SetData(new[] {color});
            return blankTexture;
        }
        
        /// <summary>
        /// Draw a colored rectangle border. 
        /// </summary>
        /// <param name="spriteBatch">Spritebatch to use for drawing.</param>
        /// <param name="rectangle">The rectangle outline.</param>
        /// <param name="color">The color of the border.</param>
        /// <param name="lineWidth">The width of the border.</param>
        public static void DrawBorder(SpriteBatch spriteBatch, Rectangle rectangle, Color color, int lineWidth)
        {
            _blankTexture ??= spriteBatch.CreateTexture(Color.White);
            var (x, y, width, height) = rectangle;
            spriteBatch.Draw(_blankTexture, new Rectangle(x, y, lineWidth, height + lineWidth), color);
            spriteBatch.Draw(_blankTexture, new Rectangle(x, y, width + lineWidth, lineWidth), color);
            spriteBatch.Draw(_blankTexture, new Rectangle(x + width, y, lineWidth, height + lineWidth), color);
            spriteBatch.Draw(_blankTexture, new Rectangle(x, y + height, width + lineWidth, lineWidth), color);
        }
    }
}