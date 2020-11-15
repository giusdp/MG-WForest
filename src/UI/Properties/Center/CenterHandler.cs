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
        public static void CenterByRow(WidgetTree tree)
        {
            var rows = GridHandler.OrganizeWidgetsInRows(tree);
            
            var verticalCenterCoord = CenteredRowsVerticalCoord(tree.Data.Space, rows.Sum(r => r.Height));
            rows.ForEach(row =>
            {
                var horizontalCenterCoord = RowHorizontalCenterCoord(tree.Data.Space, row.Width);
                row.X = horizontalCenterCoord;
                row.Y = verticalCenterCoord;
            });
            
            OffsetRowsHeights(rows);

            CenterChildren(rows, tree.Children);
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
                    var childWidget = children[i].Data;
                    CenterChildWidget(childWidget, xRow, row.Y, row.Height);

                    xRow += childWidget.Space.Size.X;
                }
            });
        }

        private static void CenterChildWidget(Widget child, int x, int y, int maxV)
        {
            if (child.Space.Size.Y != maxV) y += maxV / 2 - child.Space.Height / 2;
            child.Space = new Rectangle(new Point(x, y), child.Space.Size);
        }
        
        private static int RowHorizontalCenterCoord(Rectangle parent, int rowTotalWidth)
        {
            var (x, _, width, _) = parent;
            return (int) ((width + x) / 2f - rowTotalWidth / 2f);
        }

        private static int CenteredRowsVerticalCoord(Rectangle parent, int totalRowsHeight)
        {
            var (_, y, _, height) = parent;
            return (int) ((height + y) / 2f - totalRowsHeight / 2f);
        }
    }
}