using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PiBa.Rendering;
using PiBa.UI.Factories;
using PiBa.UI.Widgets;
using PiBa.Utilities.Collections;

namespace PiBa.UI
{
    public class HUD
    {
        private readonly Tree<Widget> _root;
        private readonly ConstraintsDict _constraints;

        private readonly SpriteBatch _spriteBatch;

        public HUD(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
            _constraints = new ConstraintsDict();
            _root = new Tree<Widget>(WidgetFactory.CreateContainer(new Rectangle(0, 0, 1280, 720)));
            _constraints.Register(_root);

            var btn = _root.AddChild(WidgetFactory.CreateSpriteButton(new Sprite("Sprite-0001")));
            _constraints.Register(btn);

            _constraints.AddConstraint(btn, ConstraintFactory.CreateCenterConstraint());
        }

        public void Update() => WidgetTreeVisitor.EnforceConstraints(_root, _constraints);

        public void Draw() => WidgetTreeVisitor.DrawTree(_root, _spriteBatch);
    }
}