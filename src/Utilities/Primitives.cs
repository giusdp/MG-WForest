using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WForest.Utilities
{
    public static class Primitives
    {
        private static Texture2D _blankTexture;

        public static Texture2D BlankTexture(this SpriteBatch s)
        {
            if (_blankTexture != null) return _blankTexture;
            _blankTexture = new Texture2D(s.GraphicsDevice, 1, 1);
            _blankTexture.SetData(new[] {Color.White});

            return _blankTexture;
        }

        public static void DrawBorder(SpriteBatch spriteBatch, Rectangle rectangle, Color color, int lineWidth)
        {
            var blank = spriteBatch.BlankTexture();
            var (x, y, width, height) = rectangle;
            spriteBatch.Draw(blank, new Rectangle(x, y, lineWidth, height + lineWidth), color);
            spriteBatch.Draw(blank, new Rectangle(x, y, width + lineWidth, lineWidth), color);
            spriteBatch.Draw(blank, new Rectangle(x + width, y, lineWidth, height + lineWidth), color);
            spriteBatch.Draw(blank, new Rectangle(x, y + height, width + lineWidth, lineWidth), color);
        }
    }
}