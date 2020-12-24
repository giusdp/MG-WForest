using System;
using System.Collections.Generic;
using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework.Graphics;
using Serilog;
using WForest.Exceptions;

namespace WForest.UI.Widgets.TextWidget
{
    public static class FontManager
    {
        internal static Font DefaultFont { get; set; }

        private static readonly Dictionary<string, Font> Fonts = new Dictionary<string, Font>();

        public static void Initialize(Font defaultFont)
        {
            DefaultFont = defaultFont; 
            Log.Information("FontManager Initialized");
        }

        internal static void RegisterFont(string name, Font font)
        {
            CheckIfInit();
            Fonts.Add(name, font);
        }

        internal static Font GetFont(string name)
        {
            CheckIfInit();
            if (Fonts.TryGetValue(name, out var font))
            {
                return font;
            }

            throw new FontNotFoundException($"The font {name} was not previously registered.");
        }

        private static void CheckIfInit()
        {
            if (DefaultFont == null) throw new FontManagerNotInitializedException();
        }
    }
}