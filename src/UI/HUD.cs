using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WForest.UI.Factories;
using WForest.UI.Widgets;
using WForest.UI.WidgetTreeHandlers;

namespace WForest.UI
{
    public class HUD
    {
        private readonly WidgetTree _root;
        private readonly WidgetTreeVisitor _widgetTreeVisitor;


        public HUD()
        {
            _widgetTreeVisitor = new WidgetTreeVisitor();

            _root = new WidgetTree(Factories.Widgets.Container(new Rectangle(0, 0, 1280, 720)));
            _root.AddProperty(Factories.Properties.Row());
            var column = _root.AddChild(Factories.Widgets.Container(1200, 700));
            column.AddProperty(Factories.Properties.Column());
            
            column.AddChild(Factories.Widgets.ImageButton("SpriteBtnL"));
            // column.AddChild(Factories.Widgets.ImageButton("SpriteBtnL"));
            
            // ((ImageButton) btn.Data).HoverButton = AssetLoader.Load<Texture2D>("Sprite-0002");
            // ((ImageButton) btn.Data).PressedButton = AssetLoader.Load<Texture2D>("Sprite-0003");

            column.AddProperty(Factories.Properties.Border(Color.Chocolate,1));
            column.AddProperty(Factories.Properties.Center());
            column.AddProperty(Factories.Properties.ItemCenter());
            // _root.AddProperty(Factories.Properties.Center());

            _widgetTreeVisitor.ApplyPropertiesOnTree(_root);
        }

        public void Update()
        {
            _widgetTreeVisitor.CheckHovering(_root, Mouse.GetState().Position);
        }

        public void Draw(SpriteBatch spriteBatch) => _widgetTreeVisitor.DrawTree(_root, spriteBatch);
    }
}