using System.Collections.Generic;
using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WForest.UI.Widgets.TextWidget
{
    public class Font
    {
        private SpriteBatch _spriteBatch;
        private FontSystem _fontSystem;
        private int size;

        public Font(SpriteBatch spriteBatch, int textureWidth, int textureHeight, List<Stream> fontFiles, int fontSize = 12)
        {
            _spriteBatch = spriteBatch;
            _fontSystem = FontSystemFactory.Create(spriteBatch.GraphicsDevice, textureWidth, textureHeight);
            size = 12;
            fontFiles.ForEach(f => _fontSystem.AddFont(f));
        }

        public Font(SpriteBatch spriteBatch, List<Stream> fontFiles, int fontSize = 12)
        {
           _spriteBatch = spriteBatch;
            _fontSystem = FontSystemFactory.Create(spriteBatch.GraphicsDevice, 1024, 1024);
            size = fontSize;
            fontFiles.ForEach(f => _fontSystem.AddFont(f)); 
        }
        

        public void DrawText(string text)
        {
            _spriteBatch.DrawString(_fontSystem.GetFont(size), text, new Vector2(0,0), Color.White);
        }
    }
}