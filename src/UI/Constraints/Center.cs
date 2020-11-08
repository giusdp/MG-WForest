using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using PiBa.UI.Widgets;
using PiBa.Utilities.Collections;

namespace PiBa.UI.Constraints
{
    public class Center : IConstraint
    {
        public bool isSatisfied { get; private set; }

        public void EnforceOn(WidgetTree widgetNode)
        {
            if (isSatisfied) return;
            if (widgetNode.Parent == null) return;
            var parentSpace = widgetNode.Parent.Data.Space;
            var size = widgetNode.Data.Space.Size;
            widgetNode.Data.Space = new Rectangle(GetLocationToCenterElementInRect(parentSpace, size), size);
            isSatisfied = true;
        }

        public static Point GetLocationToCenterElementInRect(Rectangle parent, Point size)
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