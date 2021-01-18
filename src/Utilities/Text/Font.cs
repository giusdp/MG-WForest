using System.Collections.Generic;
using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework.Graphics;

namespace WForest.Utilities.Text
{
    /// <summary>
    /// A font used to display text with the Text Widget. It uses the FontSystem from the FontStashSharp library.
    /// </summary>
    public class Font
    {

        private readonly FontSystem _fontSystem;

        /// <summary>
        /// Creates a Font with a FontSystem.
        /// </summary>
        /// <param name="fontSystem"></param>
        public Font(FontSystem fontSystem)
        {
            _fontSystem = fontSystem;
        }

        /// <summary>
        /// Creates a Font by building a FontSystem with the arguments passed.
        /// </summary>
        /// <param name="graphicsDevice"></param>
        /// <param name="fontFiles"></param>
        /// <param name="textureWidth"></param>
        /// <param name="textureHeight"></param>
        public Font(GraphicsDevice graphicsDevice, List<string> fontFiles,
            int textureWidth = 1024,
            int textureHeight = 1024)
        {
            var fontSystem = FontSystemFactory.Create(graphicsDevice, textureWidth, textureHeight);
            fontFiles.ForEach(f => fontSystem.AddFont(File.ReadAllBytes(f)));
            _fontSystem = fontSystem;
        }

        internal virtual DynamicSpriteFont SpriteFont(int fontSize) => _fontSystem.GetFont(fontSize);

        internal virtual (int, int) MeasureText(string text, int fontSize)
        {
            var (w,h) = SpriteFont(fontSize).MeasureString(text);
            return ((int, int)) (w, h);
        }
    }
}