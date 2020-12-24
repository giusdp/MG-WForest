using System.Collections.Generic;
using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WForest.UI.Widgets.TextWidget
{
    public class Font
    {
        public int Size { get; set; }

        private FontSystem _fontSystem;

        public Font(FontSystem fontSystem, int fontSize = 12)
        {
            _fontSystem = fontSystem;
            Size = fontSize;
        }

        public Font(GraphicsDevice graphicsDevice, List<string> fontFiles,
            int textureWidth = 1024,
            int textureHeight = 1024, int fontSize = 12)
        {
            var fontSystem = FontSystemFactory.Create(graphicsDevice, textureWidth, textureHeight);
            fontFiles.ForEach(f => fontSystem.AddFont(File.ReadAllBytes(f)));
            _fontSystem = fontSystem;
            Size = fontSize;
        }

        public DynamicSpriteFont SpriteFont() => _fontSystem.GetFont(Size);

    }
}