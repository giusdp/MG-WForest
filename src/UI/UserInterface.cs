using System;
using Microsoft.Xna.Framework;
using PiBa.Interfaces;
using PiBa.UI.Constraints;
using PiBa.UI.Widgets;
using PiBa.UI.Widgets.Interfaces;
using PiBa.Utilities.Collections;

namespace PiBa.UI
{
    public class UserInterface : IGameObject
    {
        private readonly Tree<IWidget> _widgetTree;

        public UserInterface()
        {
            IWidget widget = new Container(Rectangle.Empty, Rectangle.Empty);
            widget.AddConstraint(new Center());
            _widgetTree = new Tree<IWidget>(widget); 
        }

        public void Update() => ApplyToWidgetTree(_widgetTree, w => w.Update());
        

        public void Draw() => ApplyToWidgetTree(_widgetTree, w => w.Draw());
        

        private static void ApplyToWidgetTree(Tree<IWidget> tree, Action<IWidget> action)
        {
            foreach (var t in tree) action(t.Data);
        }
    }
}