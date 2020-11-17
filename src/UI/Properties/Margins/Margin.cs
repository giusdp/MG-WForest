using System;
using Microsoft.Xna.Framework;

namespace PiBa.UI.Properties.Margins
{
    public class Margin : IProperty
    {
        public int Priority { get; } = 2;
        private readonly int _margin;

        public Margin(int margin)
        {
            _margin = margin;
        }

        public void ApplyOn(WidgetTree widgetNode)
        {
            Console.WriteLine($"Margin Applied to {widgetNode}");
            var (x, y, w, h) = widgetNode.Data.Space;
            widgetNode.Data.Space = new Rectangle(x + _margin, y + _margin, w, h);
            widgetNode.Data.Margin = new Rectangle(_margin, _margin, _margin, _margin);
        }
    }
}