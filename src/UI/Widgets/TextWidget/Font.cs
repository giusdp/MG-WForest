using System.Collections.Generic;
using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework.Graphics;

namespace WForest.UI.Widgets.TextWidget
{
    public class Font
    {

        private readonly FontSystem _fontSystem;

        public Font(FontSystem fontSystem)
        {
            _fontSystem = fontSystem;
        }

        public Font(GraphicsDevice graphicsDevice, List<string> fontFiles,
            int textureWidth = 1024,
            int textureHeight = 1024)
        {
            var fontSystem = FontSystemFactory.Create(graphicsDevice, textureWidth, textureHeight);
            fontFiles.ForEach(f => fontSystem.AddFont(File.ReadAllBytes(f)));
            _fontSystem = fontSystem;
        }

        public virtual DynamicSpriteFont SpriteFont(int fontSize) => _fontSystem.GetFont(fontSize);

        public virtual (int, int) MeasureText(string text, int fontSize)
        {
            var (w,h) = SpriteFont(fontSize).MeasureString(text);
            return ((int, int)) (w, h);
        }
    }
}