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
            var rows = GridHandler.BuildRowsWithWidgets(tree);
            GridHandler.OffsetWidgetsInRows(tree.Children, rows);
            // OffsetHorizontalPositions(tree.Children, rows);
            OffsetVerticalPositions(tree.Children, rows);
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
    }
}