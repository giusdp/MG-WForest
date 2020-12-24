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

        public void DrawText(SpriteBatch spriteBatch, string text)
        {
            spriteBatch.DrawString(_fontSystem.GetFont(Size), text, new Vector2(0, 0), Color.White);
        }

        private static FontSystem CreateFont(GraphicsDevice graphicsDevice, List<string> fontFiles,
            int textureWidth = 1024,
            int textureHeight = 1024)
        {
            var fontSystem = FontSystemFactory.Create(graphicsDevice, textureWidth, textureHeight);
            fontFiles.ForEach(f => fontSystem.AddFont(File.ReadAllBytes(f)));
            return fontSystem;
        }
    }
}