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
            var columns = GridHandler.BuildColumnsWithWidgets(tree);
            GridHandler.OffsetWidgetsInColumns(tree.Children, columns);
            // OffsetVPositions(tree.Children, columns);
            OffsetHorPositions(tree.Children, columns);
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
    }
}