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
        public static void OffsetRowsHeights(List<WidgetsDataSubList> rows)
        {
            if (rows.Count <= 1) return;

            var acc = rows[0].Height;
            for (var i = 1; i < rows.Count; i++)
            {
                rows[i].Y += acc;
                acc += rows[i].Height;
            }
        }

        public static void CenterChildren(List<WidgetsDataSubList> rows, IReadOnlyList<Tree<Widget>> children)
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

        public static int RowHorizontalCenterCoord(Rectangle parent, int rowTotalWidth)
        {
            var (x, _, width, _) = parent;
            return (int) ((width + x) / 2f - rowTotalWidth / 2f);
        }

        public static int CenteredRowsVerticalCoord(Rectangle parent, int totalRowsHeight)
        {
            var (_, y, _, height) = parent;
            return (int) ((height + y) / 2f - totalRowsHeight / 2f);
        }
    }
}