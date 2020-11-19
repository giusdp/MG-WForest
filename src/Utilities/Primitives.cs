using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PiBa.Utilities
{
    public static class Primitives
    {
        private static Texture2D _pointTexture;
        
        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color, int lineWidth)
        {
            if (_pointTexture == null)
            {
                _pointTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                _pointTexture.SetData(new[] {Color.White});
            }

            var (x, y, width, height) = rectangle;
            spriteBatch.Draw(_pointTexture,
                new Rectangle(x, y, lineWidth, height + lineWidth), color);
            spriteBatch.Draw(_pointTexture,
                new Rectangle(x, y, width + lineWidth, lineWidth), color);
            spriteBatch.Draw(_pointTexture,
                new Rectangle(x + width, y, lineWidth, height + lineWidth),
                color);
            spriteBatch.Draw(_pointTexture,
                new Rectangle(x, y + height, width + lineWidth, lineWidth),
                color);
        }
    }
}