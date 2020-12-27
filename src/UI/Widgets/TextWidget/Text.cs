using System;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WForest.UI.Widgets.TextWidget
{
    public class Text : Widget
    {
        public readonly string TextString;

        public int FontSize { get; set; }

        public Font Font { get; set; }


        public Text(string text, int fontSize = 12) : base(Rectangle.Empty)
        {
            TextString = text ?? throw new ArgumentNullException();
            Font = FontManager.DefaultFont;
            FontSize = fontSize;
            var (w, h) = Font.MeasureText(TextString, FontSize);
            Space = new Rectangle(Space.X, Space.Y, w, h);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font.SpriteFont(FontSize), TextString, new Vector2(Space.X, Space.Y), Color);
            base.Draw(spriteBatch);
        }
    }
}