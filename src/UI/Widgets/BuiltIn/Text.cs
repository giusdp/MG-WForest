using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WForest.UI.Utils;
using WForest.Utilities.Text;

namespace WForest.UI.Widgets.BuiltIn
{
    /// <summary>
    /// The Text Widget. It displays strings using a Font and a size.
    /// </summary>
    public class Text : Widget
    {
        private int _size;
        private Font _font;

        /// <summary>
        /// The string to display.
        /// </summary>
        public string TextString { get; set; }

        /// <summary>
        /// The size of the text.
        /// </summary>
        public int FontSize
        {
            get => _size;
            set
            {
                _size = value;
                UpdateTextSize();
            }
        }

        /// <summary>
        /// The font to use for TextString.
        /// </summary>
        public Font Font
        {
            get => _font;
            set
            {
                _font = value;
                UpdateTextSize();
            }
        }

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
        public void Draw(SpriteBatch spriteBatch)
        {
            // spriteBatch.DrawString(Font.SpriteFont(FontSize), TextString, new Vector2(Space.X, Space.Y), Color);
        }

        private void UpdateTextSize()
        {
            var (x, y, _, _) = Space;
            var (w, h) = Font.MeasureText(TextString, FontSize);
            WidgetsSpaceHelper.UpdateSpace(this, new Rectangle(x, y, w, h));
        }
    }
}