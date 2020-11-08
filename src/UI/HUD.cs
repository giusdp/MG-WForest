using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PiBa.UI.Factories;

namespace PiBa.UI
{
    public class HUD
    {
        private readonly WidgetTree _root;

        private readonly SpriteBatch _spriteBatch;

        public HUD(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
            _root = new WidgetTree(WidgetFactory.CreateContainer(new Rectangle(0, 0, 1280, 720)));

            var btn = _root.AddChild(WidgetFactory.CreateImageButton("Sprite-0001"));

            btn.AddConstraint(ConstraintFactory.CreateCenterConstraint());
        }

        public void Update() => WidgetTreeVisitor.EnforceConstraints(_root);

        public void Draw() => WidgetTreeVisitor.DrawTree(_root, _spriteBatch);
    }
}