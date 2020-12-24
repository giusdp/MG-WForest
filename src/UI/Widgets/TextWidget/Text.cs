using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WForest.UI.Widgets.TextWidget
{
    public class Text : Widget
    {
        private string _text;

        public Font Font { get; set; }

        public Text(string text) : base(Rectangle.Empty)
        {
            _text = text;
            Font = FontManager.DefaultFont;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font.SpriteFont(), _text, new Vector2(Space.Location.X, Space.Location.Y), Color.White);
        }
    }
}