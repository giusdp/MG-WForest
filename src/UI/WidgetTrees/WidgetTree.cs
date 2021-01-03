using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WForest.UI.Properties;
using WForest.UI.Utils;
using WForest.UI.Widgets;
using WForest.Utilities.Collections;

namespace WForest.UI.WidgetTrees
{
    public class WidgetTree : Tree<Widget>
    {
        public List<Property> Properties { get; }

        public WidgetTree(Widget data) : base(data)
        {
            Properties = new List<Property>();
        }

        public WidgetTree WithProperty(Property property)
        {
            if (property == null) throw new ArgumentNullException(nameof(property));
            Properties.Add(property);
            return this;
        }

        public WidgetTree AddChild(Widget child)
        {
            if (child == null) throw new ArgumentNullException(nameof(child));
            var childNode = new WidgetTree(child) {Parent = this};
            WidgetsSpaceHelper.UpdateSpace(childNode, new Rectangle(Data.Space.Location, child.Space.Size));
            Children.Add(childNode);
            return childNode;
        }

        internal void ApplyProperties() =>
            Properties.OrderBy(p => p.Priority).ToList().ForEach(p => p.ApplyOnAndFireApplied(this));


        internal void DrawWidget(SpriteBatch spriteBatch) => Data.Draw(spriteBatch);
    }
}