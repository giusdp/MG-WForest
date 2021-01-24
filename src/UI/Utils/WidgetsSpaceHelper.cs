using Microsoft.Xna.Framework;
using WForest.UI.Widgets.Interfaces;

namespace WForest.UI.Utils
{
    internal static class WidgetsSpaceHelper
    {
        internal static void UpdateSpace(IWidget w, Rectangle newSpace)
        {
            var diff = new Point(newSpace.Location.X - w.Space.Location.X, newSpace.Location.Y - w.Space.Location.Y);
            w.Space = newSpace;
            UpdateSubTreePosition(w, diff);
        }

        private static void UpdateSubTreePosition(IWidget wt, Point diff)
        {
            foreach (var c in wt.Children)
            {
                var (dX, dY) = diff;
                var cr = new Rectangle(new Point(c.Space.X + dX, c.Space.Y + dY), c.Space.Size);
                UpdateSpace(c, cr);
            }
        }
    }
}