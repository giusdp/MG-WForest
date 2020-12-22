using System;
using System.Collections.Generic;
using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework.Graphics;

namespace WForest.UI.Widgets.TextWidget
{
    public class FontManager // TODO make this static so it's accessible from text widget and widgets factory
    {
        internal FontSystem DefaultFontSystem { get; private set; }

        private readonly Dictionary<string, FontSystem> _fontSystems = new Dictionary<string, FontSystem>();
        private readonly GraphicsDevice _graphicsDevice;

        public FontManager(GraphicsDevice graphicsDevice, List<string> fonts, int textureWidth = 1024,
            int textureHeight = 1024)
        {
            _graphicsDevice = graphicsDevice;
            DefaultFontSystem = SetupFont(fonts, textureWidth, textureHeight);
        }

        internal void AddFont(string name, List<string> fonts, int textureWidth = 1024, int textureHeight = 1024)
        {
            _fontSystems.Add(name, SetupFont(fonts, textureWidth, textureHeight));
        }

        private FontSystem SetupFont(List<string> fonts, int textureWidth = 1024, int textureHeight = 1024)
        {
            var fontSystem = FontSystemFactory.Create(_graphicsDevice, textureWidth, textureHeight);
            fonts.ForEach(f => fontSystem.AddFont(File.ReadAllBytes(f)));
            return fontSystem;
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