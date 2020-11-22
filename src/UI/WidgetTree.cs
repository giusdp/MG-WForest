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

        public void UpdateSpace(Rectangle newSpace)
        {
            var diff = new Point( newSpace.Location.X - Data.Space.Location.X, newSpace.Location.Y - Data.Space.Location.Y);
            Data.Space = newSpace;
            UpdateSubTreePosition(diff);
        }

        private void UpdateSubTreePosition(Point diff)
        {
            
            Children.ForEach(c =>
            {
                var (dX, dY) = diff;
                var cr = new Rectangle(new Point(c.Data.Space.X + dX, c.Data.Space.Y + dY), c.Data.Space.Size);
                ((WidgetTree) c).UpdateSpace(cr);
            });
        }

        public void ApplyProperties()
        {
            Properties.Distinct().ToList().OrderBy(p => p.Priority).ToList().ForEach(p => p.ApplyOn(this));
        }

        public void DrawWidget(SpriteBatch spriteBatch) => Data.Draw(spriteBatch);
    }
}