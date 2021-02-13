using System.Collections.Generic;
using Serilog;
using WForest.Exceptions;

namespace WForest.Utilities.Text
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
        /// Get or set the DefaultFont. Set the default font before using the FontStore.
        /// It will be used whenever text is used and a custom font is not required.
        /// </summary>
        /// <exception cref="FontStoreNotInitializedException"></exception>
        public static Font DefaultFont
        {
            get
            {
                CheckIfInit();
                return _defaultFont!;
            }
            set
            {
                _defaultFont = value;
                Log.Information("FontStore Default Font has been set");
            }
        }

        private static readonly Dictionary<string, Font> Fonts = new Dictionary<string, Font>();

        /// <summary>
        /// Add a new font to the store with a name associated to it that you can use to retrieve the font with GetFont.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="font"></param>
        public static void RegisterFont(string name, Font font)
        {
            CheckIfInit();
            Fonts.Add(name, font);
            Log.Information("New font registered in the Font Store");
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
                    "No default font found, assign one first to FontStore.DefaultFont");
        }
    }
}