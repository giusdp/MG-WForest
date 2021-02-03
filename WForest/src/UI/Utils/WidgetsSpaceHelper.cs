using Microsoft.Xna.Framework;
using WForest.UI.Widgets.Interfaces;
using WForest.Utilities;

namespace WForest.UI.Utils
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
                var cr = new RectangleF(c.Space.X + dX, c.Space.Y + dY, c.Space.Width, c.Space.Height);
                UpdateSpace(c, cr);
            }
        }
    }
}