using Microsoft.Xna.Framework;
using WForest.Widgets.Interfaces;

namespace WForest.Utilities.WidgetUtils
{
    internal static class WidgetsSpaceHelper
    {
        internal static void UpdateSpace(IWidget w, RectangleF newSpace)
        {
            var diff = new Vector2(newSpace.X - w.Space.X, newSpace.Y - w.Space.Y);
            w.Space = newSpace;
            UpdateSubTreePosition(w, diff);
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
    }
}