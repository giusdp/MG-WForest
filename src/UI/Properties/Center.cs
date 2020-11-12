using System.Linq;
using Microsoft.Xna.Framework;
using PiBa.UI.Widgets;

namespace PiBa.UI.Properties
{
    public class Center : IProperty
    {
        public void ApplyOn(WidgetTree widgetNode)
        {
            if (widgetNode.Children.Count == 0) return;

            var widget = widgetNode.Data;

            var maxH = SumChildrenHorizontalSizes(widgetNode);
            var maxV = GetMaxHeight(widgetNode);

            var (x, y) = GetLocationToCenterElementInRect(widget.Space, new Point(maxH, maxV));

            foreach (var child in widgetNode.Children)
            {
                var childWidget = child.Data;
                CenterChildWidget(childWidget, x, y, maxV);
                x += childWidget.Space.Size.X;
            }
        }

        private static void CenterChildWidget(Widget widget, int x, int y, int maxV)
        {
            if (widget.Space.Size.Y != maxV) y += maxV / 2 - widget.Space.Height / 2;
            widget.Space = new Rectangle(new Point(x, y), widget.Space.Size);
        }

        private static int SumChildrenHorizontalSizes(WidgetTree tree) =>
            tree.Children.Aggregate(0, (i, w) => i + w.Data.Space.Size.X);

        private static int GetMaxHeight(WidgetTree tree) => tree.Children.Max(w => w.Data.Space.Size.Y);

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