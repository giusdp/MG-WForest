using System.Collections.Generic;
using Serilog;
using WForest.Exceptions;

namespace WForest.UI.Widgets.TextWidget
{
    /// <summary>
    /// Static class to handle fonts.
    /// It's a store for the fonts with an easy to access DefaultFont.
    /// Others must be registered (RegisterFont method) to the store and can be retrieved later with GetFont.
    /// </summary>
    public static class FontStore
    {
        private static Font? _defaultFont;

        /// <summary>
        /// Get or set the DefaultFont.
        /// </summary>
        /// <exception cref="FontStoreNotInitializedException"></exception>
        public static Font DefaultFont
        {
            get
            {
                if (_defaultFont == null)
                    throw new FontStoreNotInitializedException(
                        "No DefaultFont set, was FontManager.Initialize() invoked?");
                return _defaultFont;
            }
            set => _defaultFont = value;
        }

        private static readonly Dictionary<string, Font> Fonts = new Dictionary<string, Font>();

        /// <summary>
        /// Initialize the store passing it a font which will be the default font.
        /// </summary>
        /// <param name="defaultFont"></param>
        public static void Initialize(Font defaultFont)
        {
            _defaultFont = defaultFont;
            Log.Information($"FontStore Initialized.");
        }

        /// <summary>
        /// Add a new font to the store with a name associated to it that you can use to retrieve the font with GetFont.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="font"></param>
        public static void RegisterFont(string name, Font font)
        {
            CheckIfInit();
            Fonts.Add(name, font);
            Log.Information($"Font {name} registered.");
        }

        /// <summary>
        /// Retrieve a Font from the store using the string name associated with it during registration.
        /// </summary>
        /// <param name="name">The name of the font used to register it.</param>
        /// <returns>The retrieved Font.</returns>
        /// <exception cref="FontNotFoundException"></exception>
        public static Font GetFont(string name)
        {
            CheckIfInit();
            if (Fonts.TryGetValue(name, out var font))
            {
                return font;
            }

            Log.Error(
                $"Could not retrieve font {name}, it was not found in the FontStore. Have you added it with the RegisterFont method?");
            throw new FontNotFoundException($"The font {name} was not found. Did you register it?");
        }

        private static void CheckIfInit()
        {
            if (_defaultFont == null)
                throw new FontStoreNotInitializedException(
                    "No default font found, did you forget to call FontManager.Initialize() ?");
        }
    }
}
