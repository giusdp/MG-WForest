using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Serilog;
using WForest.UI.Properties.Text;
using WForest.UI.Utils;
using WForest.UI.Widgets.TextWidget;
using WForest.UI.WidgetTree;

namespace WForest.UI
{
    public class HUD
    {
        private readonly WidgetTree.WidgetTree _root;
        private readonly WidgetTreeVisitor _widgetTreeVisitor;

        public HUD(int x, int y, int width, int height)
        {
            _widgetTreeVisitor = new WidgetTreeVisitor();

            _root = new WidgetTree.WidgetTree(Factories.Widgets.Container(width, height));
            _root.AddProperty(Factories.Properties.Column());
            _root.AddProperty(Factories.Properties.JustifyCenter());
            _root.AddProperty(Factories.Properties.ItemCenter());

            var textWidget = new Text("Test TextWidget");
            var text = _root.AddChild(textWidget);
            text.AddProperty(new FontSize(32));
            text.AddProperty(new FontFamily(FontManager.GetFont("Comfortaa-Bold")));
            text.AddProperty(Factories.Properties.Color(Color.DarkGoldenrod));
            text.AddProperty(Factories.Properties.Border());
            var textW2 = new Text("Test 2 TextWidget");
            var text2 = _root.AddChild(textW2);
            text2.AddProperty(new FontSize(20));
            var btn = Factories.Widgets.ImageButton("Sprite-0001");
            _root.AddChild(btn);
            _root.AddChild(Factories.Widgets.ImageButton("Sprite-0002"));
            // WidgetTreeVisitor.ApplyPropertiesOnTree(_root);
        }

        public void Resize(int width, int height)
        {
            _root.Data.Space = new Rectangle(_root.Data.Space.X, _root.Data.Space.Y, width, height);
            WidgetTreeVisitor.ApplyPropertiesOnTree(_root);
        }
        
        public void Update()
        {
            // Log.Debug($"_root space {_root.Data.Space}");
            // _root.Children.ForEach(c => Log.Debug($"Space of child {c.Data} is {c.Data.Space}"));
            _widgetTreeVisitor.UpdateTree(_root);
        }

        public void Draw(SpriteBatch spriteBatch) => WidgetTreeVisitor.DrawTree(_root, spriteBatch);
    }
}