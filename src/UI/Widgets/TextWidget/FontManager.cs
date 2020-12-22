using System;
using System.Collections.Generic;
using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework.Graphics;

namespace WForest.UI.Widgets.TextWidget
{
    public class FontManager
    {
        private readonly Dictionary<string, FontSystem> _fontSystems = new Dictionary<string, FontSystem>();

        private readonly GraphicsDevice _graphicsDevice;

        public FontManager(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        internal void AddFont(string name, List<string> fonts, int textureWidth, int textureHeight)
        {
            var fontSystem = FontSystemFactory.Create(_graphicsDevice, textureWidth, textureHeight);
            fonts.ForEach(f => fontSystem.AddFont(File.ReadAllBytes(f)));
            _fontSystems.Add(name, fontSystem);
        }

        internal FontSystem GetFont(string name)
        {
            if (_fontSystems.TryGetValue(name, out var fontSystem))
            {
                return fontSystem;
            }

            throw new Exception();
        }
    }
}