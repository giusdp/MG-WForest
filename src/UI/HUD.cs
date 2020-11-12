using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PiBa.UI.Factories;
using PiBa.UI.Properties;
using PiBa.UI.Widgets;
using PiBa.UI.WidgetTreeHandlers;
using Serilog;

namespace PiBa.UI
{
    public class HUD
    {
        private readonly WidgetTree _root;
        private readonly WidgetTreeVisitor _widgetTreeVisitor;

        private readonly SpriteBatch _spriteBatch;

        public HUD(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
            _widgetTreeVisitor = new WidgetTreeVisitor();

            _root = new WidgetTree(WidgetFactory.CreateContainer(new Rectangle(0, 0, 1280, 720)));

            _root.AddChild(WidgetFactory.CreateImageButton("SpriteBtnL"));
            _root.AddChild(WidgetFactory.CreateImageButton("SpriteBtnL"));
            _root.AddChild(WidgetFactory.CreateImageButton("SpriteBtnL"));
            _root.AddChild(WidgetFactory.CreateImageButton("SpriteBtnL"));
            _root.AddChild(WidgetFactory.CreateImageButton("SpriteBtnL"));
            _root.AddChild(WidgetFactory.CreateImageButton("SpriteBtnL"));
            // var btn = _root.AddChild(WidgetFactory.CreateImageButton("Sprite-0001"));
            // ((ImageButton) btn.Data).HoverButton = AssetLoader.Load<Texture2D>("Sprite-0002");
            // ((ImageButton) btn.Data).PressedButton = AssetLoader.Load<Texture2D>("Sprite-0003");


            _root.AddProperty(PropertyFactory.Center());

            _widgetTreeVisitor.ApplyProperties(_root);
        }

        public void Update()
        {
            _widgetTreeVisitor.CheckHovering(_root, Mouse.GetState().Position);
        }

        public void Draw() => _widgetTreeVisitor.DrawTree(_root, _spriteBatch);
    }
}