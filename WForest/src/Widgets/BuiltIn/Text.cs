using Microsoft.Xna.Framework;
using WForest.Rendering;
using WForest.Utilities;
using WForest.Utilities.Text;
using WForest.Utilities.WidgetUtils;

namespace WForest.Widgets.BuiltIn
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

        public Text(string text, int fontSize = 12) : base(RectangleF.Empty)
        {
            TextString = text;
            _font = FontStore.DefaultFont;
            _size = fontSize;
            UpdateTextSize();
        }

        /// <summary>
        /// Displays the TextString at the start of the parent, suing the Font and FontSize properties.
        /// If no custom Font or FontSize was set,
        /// it uses the FontStore.DefaultFont font and a size of 12.
        /// </summary>
        /// <param name="renderer"></param>
        public override void Draw(IRenderer renderer)
        {
            renderer.DrawString(Font.SpriteFont(FontSize), TextString, new Vector2(Space.X, Space.Y), Color);
            base.Draw(renderer);
        }

        private void UpdateTextSize()
        {
            var (x, y, _, _) = Space;
            var (w, h) = Font.MeasureText(TextString, FontSize);
            WidgetsSpaceHelper.UpdateSpace(this, new RectangleF(x, y, w, h));
        }
    }
}