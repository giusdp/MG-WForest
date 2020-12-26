using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WForest.UI.Properties.Text;
using WForest.UI.Widgets.TextWidget;
using WForest.UI.WidgetTree;

namespace WForest.UI
{
    public class HUD
    {
        private readonly WidgetTree.WidgetTree _root;
        private readonly WidgetTreeVisitor _widgetTreeVisitor;


        public HUD()
        {
            _widgetTreeVisitor = new WidgetTreeVisitor();

            _root = new WidgetTree.WidgetTree(Factories.Widgets.Container(new Rectangle(0, 0, 1280, 720)));
            _root.AddProperty(Factories.Properties.Row());
            _root.AddProperty(Factories.Properties.JustifyCenter());
            _root.AddProperty(Factories.Properties.ItemCenter());

            var textWidget = new Text("Test text widget");
            var text = _root.AddChild(textWidget);
            text.AddProperty(new FontSize(32));
            // text.AddProperty(new FontFamily(FontManager.GetFont("Comfortaa-Bold")));
            text.AddProperty(Factories.Properties.Color(Color.DarkGoldenrod));

            WidgetTreeVisitor.ApplyPropertiesOnTree(_root);
        }

        public void Update()
        {
            _widgetTreeVisitor.UpdateTree(_root);
            // _widgetTreeVisitor.CheckHovering(_root, Mouse.GetState().Position);
        }

        public void Draw(SpriteBatch spriteBatch) => WidgetTreeVisitor.DrawTree(_root, spriteBatch);
    }
}