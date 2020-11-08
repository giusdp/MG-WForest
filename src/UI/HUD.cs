using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PiBa.UI.Factories;
using PiBa.UI.Widgets;

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

            var btn = _root.AddChild(WidgetFactory.CreateImageButton("Sprite-0001"));
            ((ImageButton) btn.Data).HoverButton = AssetLoader.Load<Texture2D>("Sprite-0002");
            ((ImageButton) btn.Data).PressedButton = AssetLoader.Load<Texture2D>("Sprite-0003");

            btn.AddConstraint(ConstraintFactory.CreateCenterConstraint());

            _widgetTreeVisitor.EnforceConstraints(_root);
        }

        public void Update()
        { 
            // Check for hover?
        }

        public void Draw() => _widgetTreeVisitor.DrawTree(_root, _spriteBatch);
    }
}