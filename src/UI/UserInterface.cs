using System;
using Microsoft.Xna.Framework;
using PiBa.Interfaces;
using PiBa.UI.Constraints;
using PiBa.UI.Factories;
using PiBa.UI.Widgets;
using PiBa.Utilities.Collections;

namespace PiBa.UI
{
    public class UserInterface : IGameObject
    {
        private readonly Tree<Widget> _root;

        public UserInterface()
        {
            _root = new Tree<Widget>(new Container(Window.WindowView));
            
            Widget child = new Container(new Rectangle(0,0,120,120));
            
            child.AddConstraint(ConstraintFactory.CreateCenterConstraint());
            
            _root.AddChild(child);
            
        }

        public void Update() => WidgetConstraintSolver.EnforceConstraints(_root);

        public void Draw()
        {
            

        }
    }
}