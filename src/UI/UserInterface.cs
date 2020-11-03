using System;
using Microsoft.Xna.Framework;
using PiBa.Interfaces;
using PiBa.UI.Constraints;
using PiBa.UI.Factories;
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
            IWidget widget = WidgetFactory.CreateContainerWidget(Window.WindowView.Size); 
            
            IWidget childWidget = WidgetFactory.CreateContainerWidget(new Point(120,120));
            childWidget.AddConstraint(ConstraintFactory.CreateCenterCostraint(new Point(200, 150)));

            _widgetTree = new Tree<IWidget>(widget);
            _widgetTree.AddChild(childWidget);
        }

        public void Update() => WidgetConstraintSolver.EnforceConstraints(_widgetTree);

        public void Draw()
        {


        }
    }
}