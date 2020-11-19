using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WForest.UI.Properties;
using WForest.UI.Widgets;
using WForest.Utilities.Collections;

namespace WForest.UI
{
    public class WidgetTree : Tree<Widget>
    {
        public List<IProperty> Properties { get; }

        public WidgetTree(Widget data) : base(data)
        {
            Properties = new List<IProperty>();
        }

        public void AddProperty(IProperty property)
        {
            if (property == null) throw new ArgumentNullException(nameof(property));
            Properties.Add(property);
        }

        public WidgetTree AddChild(Widget child)
        {
            if (child == null) throw new ArgumentNullException(nameof(child));
            child.Space = new Rectangle(Data.Space.Location, child.Space.Size);
            var childNode = new WidgetTree(child) {Parent = this};
            Children.Add(childNode);
            return childNode;
        }

        public void ApplyProperties()
        {
            Properties.Distinct().ToList().Sort((p1, p2) => p1.Priority.CompareTo(p2.Priority));
            Properties.ForEach(c => c.ApplyOn(this));
        }

        public void DrawWidget(SpriteBatch spriteBatch) => Data.Draw(spriteBatch);
    }
}