using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using WForest.Props.Grid.StretchingProps;
using WForest.Props.Interfaces;
using WForest.Widgets.Interfaces;

namespace WForest.Utilities.WidgetUtils
{
    internal static class WidgetSpaceHelper
    {
        internal static void UpdateSpace(IWidget w, RectangleF newSpace)
        {
            var diff = new Vector2(newSpace.X - w.Space.X, newSpace.Y - w.Space.Y);
            w.Space = newSpace;
            UpdateSubTreePosition(w, diff);
            ResetStretching(GetChildrenWithStretchProps(w));
        }

        private static void UpdateSubTreePosition(IWidget wt, Vector2 diff)
        {
            foreach (var c in wt.Children)
            {
                var dX = diff.X;
                var dY = diff.Y;
                var (x, y, w, h) = c.TotalSpaceOccupied;
                var cr = new RectangleF(x + dX, y + dY, w, h);
                UpdateSpace(c, cr);
            }
        }

        private static IReadOnlyCollection<(IWidget, List<IApplicableProp>)> GetChildrenWithStretchProps(IWidget widget)
        {
            var childWithStretch = new List<(IWidget, List<IApplicableProp>)>();

            foreach (var c in widget.Children)
            {
                var l = new List<IApplicableProp>();
                var hb = c.Props.SafeGetByProp<HorizontalStretch>().TryGetValue(out var hl);
                if (hb)
                    l.AddRange(hl.Cast<IApplicableProp>());

                var vb = c.Props.SafeGetByProp<VerticalStretch>().TryGetValue(out var vl);
                if (vb)
                    l.AddRange(vl.Cast<IApplicableProp>());

                childWithStretch.Add((c, l));
            }

            return childWithStretch;
        }

        private static void ResetStretching(IReadOnlyCollection<(IWidget, List<IApplicableProp>)> widgetStretchProps)
        {
            foreach (var childrenStretches in widgetStretchProps)
            {
                var (_, stretchProps) = childrenStretches;
                foreach (var prop in stretchProps.Where(prop => prop.IsApplied)) prop.IsApplied = false;
            }

            foreach (var childrenStretches in widgetStretchProps)
            {
                var (child, stretchProps) = childrenStretches;
                foreach (var prop in stretchProps.Where(prop => !prop.IsApplied)) prop.ApplyOn(child);
            }
        }
    }
}