using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WForest.Utilities
{
    public static class Primitives
    {
        private static Texture2D _blankTexture;
        public static Texture2D CreateTexture(this SpriteBatch s, Color color)
        {
            var blankTexture = new Texture2D(s.GraphicsDevice, 1, 1);
            blankTexture.SetData(new[] {color});
            return blankTexture;
        }
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