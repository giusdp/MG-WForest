using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WForest.Devices;
using WForest.UI.Properties;
using WForest.UI.Properties.Dragging;
using WForest.UI.Widgets;
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

            var slider = _root.AddChild(Factories.Widgets.Block(200,5));
            slider.AddProperty(Factories.Properties.Color(Color.DarkBlue));
            // slider.AddProperty(Factories.Properties.Margin(0,0,30,30));
            slider.AddProperty(Factories.Properties.Column());
            // slider.AddProperty(Factories.Properties.JustifyCenter());
            slider.AddProperty(Factories.Properties.ItemCenter());
            var sld = slider.AddChild(Factories.Widgets.Block(20, 20));
            sld.AddProperty(Factories.Properties.Color(Color.Aqua));
            sld.AddProperty(new Draggable(new MouseDevice()));
            sld.AddProperty(new FixY());
            
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