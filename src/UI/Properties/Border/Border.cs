using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PiBa.UI.Properties.Border
{
    public class Border : IProperty
    {
        private static Texture2D _pointTexture;
        private Color _color;
        private int _lineWidth;

        public Border()
        {
            _lineWidth = 1;
            _color = Color.Black;
        }

        public Border(int lineWidth)
        {
            _lineWidth = lineWidth;
            _color = Color.Black;
        }

        public Border(Color color, int lineWidth)
        {
            _color = color;
            _lineWidth = lineWidth;
        }

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

        public int Priority { get; } = 0;

        public void ApplyOn(WidgetTree widgetNode)
        {
            widgetNode.Data.Modifiers.Add(sb => { DrawRectangle(sb, widgetNode.Data.Space, _color, _lineWidth); });
        }
    }
}