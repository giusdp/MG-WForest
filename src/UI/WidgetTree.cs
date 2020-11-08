using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PiBa.UI.Constraints;
using PiBa.UI.Widgets;
using PiBa.Utilities.Collections;

namespace PiBa.UI
{
    public class WidgetTree : Tree<Widget>
    {
        public List<IConstraint> Constraints { get; }

        public WidgetTree(Widget data) : base(data)
        {
            Constraints = new List<IConstraint>();
        }

        public void AddConstraint(IConstraint constraint)
        {
            if (constraint == null) throw new ArgumentNullException(nameof(constraint));
            Constraints.Add(constraint);
        }

        public WidgetTree AddChild(Widget child)
        {
            if (child == null) throw new ArgumentNullException(nameof(child));
            var childNode = new WidgetTree(child) {Parent = this};
            Children.Add(childNode);
            return childNode;
        }

        public void EnforceConstraints()
            => Constraints.ForEach(c => c.EnforceOn(this));

        public void DrawWidget(SpriteBatch spriteBatch) => Data.Draw(spriteBatch);
        public bool isHovered(Point mouseLoc) => Data.IsHovered(mouseLoc);
    }
}