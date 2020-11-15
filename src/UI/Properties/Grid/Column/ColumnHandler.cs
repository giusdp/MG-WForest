using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using PiBa.UI.Widgets;
using PiBa.Utilities.Collections;

namespace PiBa.UI.Properties.Grid.Column
{
    public static class ColumnHandler
    {
        public static void SetWidgetsInRows(WidgetTree tree)
        {
            var columns = BuildColumnsWithWidgets(tree);
            OffsetVPositions(tree.Children, columns);
            OffsetHorPositions(tree.Children, columns);
        }

        private static void OffsetVPositions(List<Tree<Widget>> widgetTrees, List<WidgetsDataSubList> cols)
        {
            if (cols.Count == 0) return;

            var widgets = widgetTrees.Select(t => t.Data).ToList();

            cols.ForEach(row =>
            {
                var accY = widgets[row.FirstWidgetIndex].Space.Height;
                for (var i = row.FirstWidgetIndex + 1; i < row.LastWidgetIndex; i++)
                {
                    var (x, y, width, height) = widgets[i].Space;
                    widgets[i].Space = new Rectangle(x, y + accY, width, height);
                    accY += widgets[i].Space.Height;
                }
            });
        }

        private static void OffsetHorPositions(List<Tree<Widget>> widgetTrees, List<WidgetsDataSubList> cols)
        {
            if (cols.Count <= 1) return;

            var widgets = widgetTrees.Select(t => t.Data).ToList();
            var acc = cols[0].Width;
            for (var i = 1; i < cols.Count; i++)
            {
                for (var j = cols[i].FirstWidgetIndex; j < cols[i].LastWidgetIndex; j++)
                {
                    var (x, y, width, height) = widgets[j].Space;
                    widgets[j].Space = new Rectangle(x + acc, y, width, height);
                }

                acc += cols[i].Width;
            }
        }


        private static List<WidgetsDataSubList> BuildColumnsWithWidgets(WidgetTree widget)
        {
            var columns = new List<WidgetsDataSubList>();
            var previousIndex = 0;

            var done = false;
            while (!done)
            {
                var (maxVert, firstIndexOnNewRow) =
                    GridHandler.SumChildrenHeightsTilFit(widget.Children, previousIndex, widget.Data.Space.Size.Y);

                var maxHor = GetMaxWidthInChildrenSubList(widget.Children, previousIndex, firstIndexOnNewRow);

                var col = new WidgetsDataSubList(maxHor, maxVert, previousIndex,
                    firstIndexOnNewRow < 0 ? widget.Children.Count : firstIndexOnNewRow);

                columns.Add(col);
                previousIndex = firstIndexOnNewRow;
                done = firstIndexOnNewRow == -1;
            }

            return columns;
        }

        private static int GetMaxWidthInChildrenSubList(List<Tree<Widget>> children, int from, int until) =>
            children.GetRange(from, until < 0 ? children.Count - from : until - from).Max(w => w.Data.Space.Size.X);

    }
}