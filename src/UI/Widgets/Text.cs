using System;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WForest.Utilities.Text;

namespace WForest.UI.Widgets
{
    /// <summary>
    /// The Text Widget. It displays strings using a Font and a size.
    /// </summary>
    public class Text : Widget
    {
        /// <summary>
        /// The string to display.
        /// </summary>
        public string TextString { get; set; }

        /// <summary>
        /// The size of the text.
        /// </summary>
        public int FontSize { get; set; }

        /// <summary>
        /// The font to use for TextString.
        /// </summary>
        public Font Font { get; set; }

        internal Text(string text, int fontSize = 12) : base(Rectangle.Empty)
        {
            TextString = text ?? throw new ArgumentNullException();
            Font = FontStore.DefaultFont;
            FontSize = fontSize;
            var (w, h) = Font.MeasureText(TextString, FontSize);
            Space = new Rectangle(Space.X, Space.Y, w, h);
        }

        /// <summary>
        /// Displays the TextString at the start of the parent, suing the Font and FontSize properties.
        /// If no custom Font or FontSize was set,
        /// it uses the FontStore.DefaultFont font and a size of 12.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font.SpriteFont(FontSize), TextString, new Vector2(Space.X, Space.Y), Color);
        }
    }
}