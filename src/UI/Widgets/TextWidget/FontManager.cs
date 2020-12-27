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
            Log.Information($"FontManager Initialized.");
        }

        public static void RegisterFont(string name, Font font)
        {
            CheckIfInit();
            Fonts.Add(name, font);
            Log.Information($"Font {name} registered.");
        }

        public static Font GetFont(string name)
        {
            CheckIfInit();
            if (Fonts.TryGetValue(name, out var font))
            {
                return font;
            }

            Log.Error($"Could not retrieve font {name}, it was not found in FontManger. Have you added it with the RegisterFont method?");
            throw new FontNotFoundException($"The font {name} not found. Did you forget to register it?");
        }

        private static void CheckIfInit()
        {
            if (_defaultFont == null) throw new FontManagerNotInitializedException("No default font found, did you forget to call FontManager.Initialize() ?");
        }
    }
}