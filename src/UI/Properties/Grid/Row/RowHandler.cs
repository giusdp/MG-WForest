using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using PiBa.UI.Widgets;
using PiBa.Utilities.Collections;

namespace PiBa.UI.Properties.Grid.Row
{
    public static class RowHandler
    {
        public static void SetWidgetsInRows(WidgetTree tree)
        {
            var rows = BuildRowsWithWidgets(tree);
            OffsetHorizontalPositions(tree.Children, rows);
            OffsetVerticalPositions(tree.Children, rows);
        }

        private static void OffsetHorizontalPositions(List<Tree<Widget>> widgetTrees, List<WidgetsDataSubList> rows)
        {
            if (rows.Count == 0) return;

            var widgets = widgetTrees.Select(t => t.Data).ToList();

            rows.ForEach(row =>
            {
                var accX = widgets[row.FirstWidgetIndex].Space.Width;
                for (var i = row.FirstWidgetIndex + 1; i < row.LastWidgetIndex; i++)
                {
                    var (x, y, width, height) = widgets[i].Space;
                    widgets[i].Space = new Rectangle(x + accX, y, width, height);
                    accX += widgets[i].Space.Width;
                }
            });
        }

        private static void OffsetVerticalPositions(List<Tree<Widget>> widgetTrees, List<WidgetsDataSubList> rows)
        {
            if (rows.Count <= 1) return;

            var widgets = widgetTrees.Select(t => t.Data).ToList();
            var acc = rows[0].Height;
            for (var i = 1; i < rows.Count; i++)
            {
                for (var j = rows[i].FirstWidgetIndex; j < rows[i].LastWidgetIndex; j++)
                {
                    var (x, y, width, height) = widgets[j].Space;
                    widgets[j].Space = new Rectangle(x, y + acc, width, height);
                }

                acc += rows[i].Height;
            }
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

            return rows;
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
    }
}