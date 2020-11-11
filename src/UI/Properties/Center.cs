using System.Linq;
using Microsoft.Xna.Framework;
using Serilog;

namespace PiBa.UI.Properties
{
    public class Center : IProperty
    {
        public void ApplyOn(WidgetTree widgetNode)
        {
            if (widgetNode.Children.Count == 0) return;

            var widget = widgetNode.Data;

            var maxH = widgetNode.Children.Aggregate(0, (i, w) => i + w.Data.Space.Size.X);
            var maxV = widgetNode.Children.Max(w => w.Data.Space.Size.Y);

            var (x, y) = GetLocationToCenterElementInRect(widget.Space, new Point(maxH, maxV));

            foreach (var child in widgetNode.Children)
            {
                var childWidget = child.Data;
                var newY = y;
                
                if (childWidget.Space.Size.Y != maxV) newY += maxV/2 - childWidget.Space.Height / 2;
                
                childWidget.Space = new Rectangle(new Point(x, newY), childWidget.Space.Size);
                x += childWidget.Space.Size.X;
            }
        }

        public static Point GetLocationToCenterElementInRect(Rectangle parent, Point size)
        {
            var (x, y, width, height) = parent;
            var (w, h) = size;
            return new Point(
                (int) ((width + x) / 2f - w / 2f),
                (int) ((height + y) / 2f - h / 2f)
            );
        }
    }
}