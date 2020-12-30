using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Serilog;
using WForest.UI.WidgetTree;

namespace WForest.UI
{
    public class HUD
    {
        private readonly WidgetTree.WidgetTree _root;
        private readonly WidgetTreeVisitor _widgetTreeVisitor;

        public HUD(int x, int y, int width, int height)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
            
            Log.Information("HUD instance created.");
            _widgetTreeVisitor = new WidgetTreeVisitor();

            _root = new WidgetTree.WidgetTree(Factories.Widgets.Container(new Rectangle(x,y,width, height)));
            Resize(width, height);
        }

        public void Resize(int width, int height)
        {
            _root.Data.Space = new Rectangle(_root.Data.Space.X, _root.Data.Space.Y, width, height);
            WidgetTreeVisitor.ApplyPropertiesOnTree(_root);
        }
        
        public void Update() => _widgetTreeVisitor.UpdateTree(_root);

        public void Draw(SpriteBatch spriteBatch) => WidgetTreeVisitor.DrawTree(_root, spriteBatch);
    }
}