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
           
            var btn = _root.AddChild(Factories.Widgets.ImageButton("Sprite-0001"));
            // ((ImageButton) btn.Data).HoverButton = AssetLoader.Load<Texture2D>("Sprite-0002");
            // ((ImageButton) btn.Data).PressedButton = AssetLoader.Load<Texture2D>("Sprite-0003");

            var cont = _root.AddChild(Factories.Widgets.Container(128, 64));
            // btn.AddProperty(PropertyFactory.OnEnter(()=>Console.WriteLine("Hello from container boi")));

            cont.AddProperty(Factories.Properties.Border(Color.Red, 3));
            _root.AddProperty(Factories.Properties.Row());
            _root.AddProperty(Factories.Properties.Center());

            _widgetTreeVisitor.ApplyPropertiesOnTree(_root);
        }

        public void Update()
        {
            _widgetTreeVisitor.CheckHovering(_root, Mouse.GetState().Position);
        }

        public void Draw(SpriteBatch spriteBatch) => _widgetTreeVisitor.DrawTree(_root, spriteBatch);
    }
}