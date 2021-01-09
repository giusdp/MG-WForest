using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using WForest.Exceptions;
using WForest.UI;
using WForest.UI.WidgetTrees;
using WForest.Utilities.Collections;

namespace WForest.Factories
{
    /// <summary>
    /// Entry point of the WForest Library.
    /// Initialize it to prepare the library and use the factory to create WTreeManagers,
    /// where each one is an usable instance of a menu.
    /// </summary>
    public static class WForestFactory
    {
        private static bool _isInit;

        public static void Initialize(bool isLoggingActive)
        {
            // ShaderDb.GraphicsDevice = graphicsDevice;

            var ls = new LoggingLevelSwitch(isLoggingActive ? LogEventLevel.Debug : LogEventLevel.Fatal);
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .MinimumLevel.ControlledBy(ls)
                .CreateLogger();
            Log.Information("WForest initialized, ready to create Widgets Trees.");
            _isInit = true;
        }

        public static WTreeManager CreateWTree(int x, int y, int width, int height, WidgetTree wTree)
        {
            if (wTree == null) throw new ArgumentNullException(nameof(wTree));
            if (!_isInit)
                throw new WForestNotInitializedException("Tried to create a widget tree without initializing WForest");
            return new WTreeManager(x, y, width, height, wTree);
        }

        public static WTreeManager CreateWTree(Rectangle windowSpace, WidgetTree wTree)
        {
            if (windowSpace == null) throw new ArgumentNullException(nameof(windowSpace));
            if (wTree == null) throw new ArgumentNullException(nameof(wTree));
            if (!_isInit)
                throw new WForestNotInitializedException("Tried to create a widget tree without initializing WForest");
            return new WTreeManager(windowSpace.X, windowSpace.Y, windowSpace.Width, windowSpace.Height, wTree);
        }
    }
}