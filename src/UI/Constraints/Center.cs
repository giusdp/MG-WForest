using Microsoft.Xna.Framework;
using PiBa.UI.Widgets;
using PiBa.Utilities.Collections;

namespace PiBa.UI.Constraints
{
    public class Center : IConstraint
    {
        public void EnforceOn(Tree<Widget> widgetNode)
        {
            var parentSpace = widgetNode.Parent.Data.Props.Space;
            var size = widgetNode.Data.Props.Space.Size;
            widgetNode.Data.Props.Space = new Rectangle(GetCenterLocation(parentSpace, size), size);
        }

        private static Point GetCenterLocation(Rectangle parent, Point size)
        {
            var (x, y, width, height) = parent;
            var (w, h) = size;
            return new Point(
                (int) ((width - x) / 2f - w / 2f),
                (int) ((height - y) / 2f - h / 2f)
            );
        }
    }
}