using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PiBa.Interfaces;
using PiBa.UI.Factories;
using PiBa.UI.Widgets;
using PiBa.Utilities.Collections;

namespace PiBa.UI
{
    public class UserInterface : IGameObject
    {
        private readonly Tree<Widget> _root;
        private readonly ConstraintsMap _constraints;

        private readonly SpriteBatch _spriteBatch;

        public UserInterface(SpriteBatch spriteBatch)
        {
            // _spriteBatch = spriteBatch;
            // _constraints = new ConstraintsMap();
            // _root = new Tree<Widget>(new Container(Window.WindowView));
            //
            // _constraints.Register(_root);

            // var child = _root.AddChild(new Button(new Rectangle(0, 0, 120, 120)));
            // _constraints.Register(child);
            // _constraints.AddConstraint(child, ConstraintFactory.CreateCenterConstraint());
        }

        public void Update() => WidgetTreeVisitor.EnforceConstraints(_root, _constraints);

        public void Draw() => WidgetTreeVisitor.DrawTree(_root, _spriteBatch);
    }
}