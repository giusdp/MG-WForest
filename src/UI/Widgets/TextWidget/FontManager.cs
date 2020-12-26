using System.Collections.Generic;
using Serilog;
using WForest.Exceptions;

namespace WForest.UI.Widgets.TextWidget
{
    public static class FontManager
    {
        private static Font _defaultFont;

        public static Font DefaultFont
        {
            get
            {
                CheckIfInit();
                return _defaultFont;
            }
            set => _defaultFont = value;
        }

        private static readonly Dictionary<string, Font> Fonts = new Dictionary<string, Font>();

        public static void Initialize(Font defaultFont)
        {
            _defaultFont = defaultFont; 
            Log.Information("FontManager Initialized");
        }

        public static void RegisterFont(string name, Font font)
        {
            CheckIfInit();
            Fonts.Add(name, font);
        }

        public static Font GetFont(string name)
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
            if (_defaultFont == null) throw new FontManagerNotInitializedException("No default font found, did you forget to call FontManager.Initialize() ?");
        }
    }
}