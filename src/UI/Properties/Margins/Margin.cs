using System;
using Microsoft.Xna.Framework;
using PiBa.UI.Widgets;
using PiBa.Utilities;
using PiBa.Utilities.Collections;

namespace PiBa.UI.Properties.Margins
{
    public class Margin : IProperty
    {
        public int Priority { get; } = 0;
        private readonly int _margin;

        public Margin(int margin)
        {
            _margin = margin;
        }

        public void ApplyOn(WidgetTree widgetNode)
        {
            var (x, y, w, h) = widgetNode.Data.Space;
            widgetNode.Data.Space = new Rectangle(x + _margin, y + _margin, w, h);
            widgetNode.Data.Margin = new Widgets.Margin(_margin, _margin, _margin, _margin);
            TreeVisitor<Widget>.ApplyToTreeFromRoot(widgetNode, node => node.Data.Space = new Rectangle(widgetNode.Data.Space.Location, node.Data.Space.Size));
        }
    }
}