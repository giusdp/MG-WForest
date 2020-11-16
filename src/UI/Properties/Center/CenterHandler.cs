using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using PiBa.UI.Properties.Grid;
using PiBa.UI.Widgets;
using PiBa.Utilities.Collections;

namespace PiBa.UI.Properties.Center
{
    internal static class CenterHandler
    {
        public static void CenterByRow(WidgetTree wTree, List<WidgetsDataSubList> rows)
        {
            var (x, y) = GetCenterCoords(wTree.Data.Space, rows);

            rows.ForEach(r =>
            {
                r.X = x;
                r.Y = y;
            });

            OffsetRowsHeights(rows);
            CenterChildren(rows, wTree.Children);
        }

        private static (int, int) GetCenterCoords(Rectangle space, List<WidgetsDataSubList> rows)
        {
            var maxWidth = rows.Max(r => r.Width);
            var totalHeight = rows.Sum(r => r.Height);
            var (x, y, width, height) = space;
            var centerX = (int) ((width + x) / 2f - maxWidth / 2f);
            var centerY = (int) ((height + y) / 2f - totalHeight / 2f);
            return (centerX, centerY);
        }

        private static void OffsetRowsHeights(List<WidgetsDataSubList> rows)
        {
            if (rows.Count <= 1) return;

            var acc = rows[0].Height;
            for (var i = 1; i < rows.Count; i++)
            {
                rows[i].Y += acc;
                acc += rows[i].Height;
            }
        }

        private static void CenterChildren(List<WidgetsDataSubList> rows, IReadOnlyList<Tree<Widget>> children)
        {
            rows.ForEach(row =>
            {
                var xRow = row.X;
                for (var i = row.FirstWidgetIndex; i < row.LastWidgetIndex; i++)
                {
                    var child = children[i].Data;
                    child.Space = new Rectangle(new Point(xRow, row.Y), child.Space.Size);
                    xRow += child.Space.Size.X;
                }
            });
        }
    }
}