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

    /// <summary>
    /// Represents a node with children holding widgets and their properties.
    /// </summary>
    public class WidgetTree : Tree<Widget>
    {
        /// <summary>
        /// Properties of the widget associated with this node.
        /// </summary>
        public List<Property> Properties { get; }

        /// <summary>
        /// Creates a WidgetTree holding the widget data.
        /// </summary>
        /// <param name="data"></param>
        public WidgetTree(Widget data) : base(data)
        {
            Properties = new List<Property>();
        }

        /// <summary>
        /// Add a property to the widget of this node.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public WidgetTree WithProperty(Property property)
        {
            if (property == null) throw new ArgumentNullException(nameof(property));
            Properties.Add(property);
            return this;
        }

        /// <summary>
        /// Add a child to this WidgetTree. Returns the relative WidgetTree holding the child widget..
        /// </summary>
        /// <param name="child"></param>
        /// <returns>A WidgetTree containing the child Widget.</returns>
        /// <exception cref="ArgumentNullException"></exception>
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


        internal void DrawWidget(SpriteBatch spriteBatch) => Data.DrawAndPostDraw(spriteBatch);
    }
}