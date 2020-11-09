using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using PiBa.UI.Properties;
using PiBa.UI.Widgets;
using PiBa.Utilities.Collections;

namespace PiBa.UI
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
            var childNode = new WidgetTree(child) {Parent = this};
            Children.Add(childNode);
            return childNode;
        }

        public void EnforceConstraints()
            => Properties.ForEach(c => c.ApplyOn(this));

        public void DrawWidget(SpriteBatch spriteBatch) => Data.Draw(spriteBatch);

    }
}