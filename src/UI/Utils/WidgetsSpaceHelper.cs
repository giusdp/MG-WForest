using Microsoft.Xna.Framework;
using WForest.UI.Widgets;
using WForest.Utilities.Collections;

namespace WForest.UI.Utils
{
    public static class WidgetsSpaceHelper
    {
        public static void UpdateSpace(Tree<Widget> wt, Rectangle newSpace)
        {
            Widget w = wt.Data;
            var diff = new Point(newSpace.Location.X - w.Space.Location.X, newSpace.Location.Y - w.Space.Location.Y);
            w.Space = newSpace;
            UpdateSubTreePosition(wt, diff);
        }

        private static void UpdateSubTreePosition(Tree<Widget> wt, Point diff)
        {
            wt.Children.ForEach(c =>
            {
                var (dX, dY) = diff;
                var cr = new Rectangle(new Point(c.Data.Space.X + dX, c.Data.Space.Y + dY), c.Data.Space.Size);
                UpdateSpace(c, cr);
            });
        }
    }
}