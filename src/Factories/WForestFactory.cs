using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using WForest.UI;
using WForest.UI.WidgetTree;
using WForest.Utilities.Collections;

namespace WForest.Factories
{
    public class WForestFactory
    {

        public WForestFactory(GraphicsDevice graphicsDevice, bool isLoggingActive)
        {
            ShaderDb.GraphicsDevice = graphicsDevice;

            var ls = new LoggingLevelSwitch(isLoggingActive ? LogEventLevel.Debug : LogEventLevel.Fatal);
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .MinimumLevel.ControlledBy(ls)
                .CreateLogger();
            Log.Information("WForest initialized, ready to create Widgets Trees.");
        }

        public WTreeManager CreateWTree(int x, int y, int width, int height, WidgetTree wTree) =>
            new WTreeManager(x, y, width, height, wTree);

        public WTreeManager CreateWTree(Rectangle windowSpace, WidgetTree wTree) =>
            new WTreeManager(windowSpace.X, windowSpace.Y, windowSpace.Width, windowSpace.Height, wTree);
    }
}