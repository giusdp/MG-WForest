using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WForest.UI.Properties;
using WForest.UI.Utils;
using WForest.UI.Widgets;
using WForest.UI.Widgets.Interactions;
using WForest.Utilities.Collections;

namespace WForest.UI.WidgetTree
{
    public class WidgetTree : Tree<Widget>
    {

        public Interaction CurrentInteraction { get; set; }
        public List<Property> Properties { get; }

        public WidgetTree(Widget data) : base(data)
        {
            Properties = new List<Property>();
            CurrentInteraction = Interaction.Untouched;
        }

        public void AddProperty(Property property)
        {
            if (property == null) throw new ArgumentNullException(nameof(property));
            Properties.Add(property);
        }

        public WidgetTree AddChild(Widget child)
        {
            if (child == null) throw new ArgumentNullException(nameof(child));
            var childNode = new WidgetTree(child) {Parent = this};
            WidgetsSpaceHelper.UpdateSpace(childNode, new Rectangle(Data.Space.Location, child.Space.Size));
            Children.Add(childNode);
            return childNode;
        }

        public void ApplyProperties()
        {
            Properties.Distinct().OrderBy(p => p.Priority).ToList().ForEach(p => p.ApplyOnAndFireApplied(this));
        }

        public void DrawWidget(SpriteBatch spriteBatch) => Data.Draw(spriteBatch);

    }
}