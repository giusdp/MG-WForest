using System;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using WForest.Devices;
using WForest.Exceptions;
using WForest.UI;
using WForest.UI.Widgets.Interfaces;

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

        /// <summary>
        /// Initializes the library and sets up a logger that will be used to log information or errors
        /// if isLoggingActive passed is true.
        /// </summary>
        /// <param name="isLoggingActive"></param>
        public static void Initialize(bool isLoggingActive)
        {
            // ShaderDb.GraphicsDevice = graphicsDevice;

            var ls = new LoggingLevelSwitch(isLoggingActive ? LogEventLevel.Debug : LogEventLevel.Fatal);
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .MinimumLevel.ControlledBy(ls)
                .CreateLogger();
            Log.Information("WForest initialized, ready to create Widgets Trees");
            _isInit = true;
        }

        /// <summary>
        /// Creates a <see cref="WTreeManager"/> that covers the area defined by the space of the root widget and handles
        /// resizing, updating and drawing the widget tree. By default it's given a MouseDevice.Instance to check for
        /// interactions with the widgets.
        /// </summary>
        /// <param name="wTree"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="WForestNotInitializedException"></exception>
        public static WTreeManager CreateWTree(IWidget wTree)
        {
            if (wTree == null) throw new ArgumentNullException(nameof(wTree));
            if (!_isInit)
                throw new WForestNotInitializedException("Tried to create a widget tree without initializing WForest");
            return new WTreeManager(wTree, MouseDevice.Instance);
        }

        /// <summary>
        /// Creates a <see cref="WTreeManager"/> that covers the area defined by the space of the root widget and handles
        /// resizing, updating and drawing the widget tree. The IDevice in input is used to check for interactions with the widgets.
        /// </summary>
        /// <param name="wTree"></param>
        /// <param name="device"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="WForestNotInitializedException"></exception>
        public static WTreeManager CreateWTree(IWidget wTree, IDevice device)
        {
            if (wTree == null) throw new ArgumentNullException(nameof(wTree));
            if (!_isInit)
                throw new WForestNotInitializedException("Tried to create a widget tree without initializing WForest");
            return new WTreeManager(wTree, device);
        }
        // /// <summary>
        // /// Creates a <see cref="WTreeManager"/> that covers the area passed as input and handles the given
        // /// <see cref="WidgetTree"/>.
        // /// </summary>
        // /// <param name="x"></param>
        // /// <param name="y"></param>
        // /// <param name="width"></param>
        // /// <param name="height"></param>
        // /// <param name="wTree"></param>
        // /// <returns></returns>
        // /// <exception cref="ArgumentNullException"></exception>
        // /// <exception cref="WForestNotInitializedException"></exception>
        // public static WTreeManager CreateWTree(int x, int y, int width, int height, IWidget wTree)
        // {
        //     if (wTree == null) throw new ArgumentNullException(nameof(wTree));
        //     if (!_isInit)
        //         throw new WForestNotInitializedException("Tried to create a widget tree without initializing WForest");
        //     return new WTreeManager(x, y, width, height, wTree);
        // }

        // /// <summary>
        // /// Creates a <see cref="WTreeManager"/> that covers the area passed as input and handles the given
        // /// <see cref="WidgetTree"/>. 
        // /// </summary>
        // /// <param name="windowSpace"></param>
        // /// <param name="wTree"></param>
        // /// <returns></returns>
        // /// <exception cref="ArgumentNullException"></exception>
        // /// <exception cref="WForestNotInitializedException"></exception>
        // public static WTreeManager CreateWTree(Rectangle windowSpace, IWidget wTree)
        // {
        //     if (windowSpace == null) throw new ArgumentNullException(nameof(windowSpace));
        //     if (wTree == null) throw new ArgumentNullException(nameof(wTree));
        //     if (!_isInit)
        //         throw new WForestNotInitializedException("Tried to create a widget tree without initializing WForest");
        //     return new WTreeManager(windowSpace.X, windowSpace.Y, windowSpace.Width, windowSpace.Height, wTree);
        // }
    }
}