using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

            // var c = _root.AddChild(new Block(new Rectangle(0, 0, 256, 44)));
            var btn = Factories.Widgets.ImageButton("Sprite-0001");
            ((ImageButton) btn).HoverButton = AssetLoader.Load<Texture2D>("Sprite-0002");
            ((ImageButton) btn).PressedButton = AssetLoader.Load<Texture2D>("Sprite-0003");
            var c = _root.AddChild(btn);
            c.AddProperty(Factories.Properties.Color(Color.Yellow));
            c.AddProperty(Factories.Properties.Rounded(22));
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