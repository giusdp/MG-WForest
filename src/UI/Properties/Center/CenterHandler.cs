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
            var rows = BuildRowsWithWidgets(tree);

            OffsetRowsHeights(rows);

            CenterChildren(rows, tree.Children);
        }

        private static List<WidgetsDataSubList> BuildRowsWithWidgets(WidgetTree widget)
        {
            var rows = new List<WidgetsDataSubList>();
            var previousIndex = 0;

            var done = false;
            while (!done)
            {
                var (maxH, firstIndexOnNewRow) =
                    SumChildrenWidthsTilFit(widget.Children, previousIndex, widget.Data.Space.Size.X);

                var maxV = GetMaxHeightInChildrenSubList(widget.Children, previousIndex, firstIndexOnNewRow);

                var row = new WidgetsDataSubList(maxH, maxV, previousIndex,
                    firstIndexOnNewRow < 0 ? widget.Children.Count : firstIndexOnNewRow);

                rows.Add(row);
                previousIndex = firstIndexOnNewRow;
                done = firstIndexOnNewRow == -1;
            }

            var verticalCenterCoord = CenteredRowsVerticalCoord(widget.Data.Space, rows.Sum(r => r.Height));
            rows.ForEach(row =>
            {
                var horizontalCenterCoord = RowHorizontalCenterCoord(widget.Data.Space, row.Width);
                row.X = horizontalCenterCoord;
                row.Y = verticalCenterCoord;
            });
            return rows;
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

        private static (int, int) SumChildrenWidthsTilFit(List<Tree<Widget>> children, int startIndex, int parentHSize)
        {
            var acc = 0;
            var indexOnNewRow = -1;

            for (var i = startIndex; i < children.Count; i++)
            {
                if (acc + children[i].Data.Space.Size.X > parentHSize)
                {
                    indexOnNewRow = i;
                    break;
                }

                acc += children[i].Data.Space.Size.X;
            }

            return (acc, indexOnNewRow);
        }

        private static int GetMaxHeightInChildrenSubList(List<Tree<Widget>> children, int from, int until) =>
            children.GetRange(from, until < 0 ? children.Count - from : until - from).Max(w => w.Data.Space.Size.Y);

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