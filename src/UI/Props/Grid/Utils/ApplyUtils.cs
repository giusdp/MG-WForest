using System;
using System.Collections.Generic;
using Serilog;
using WForest.UI.WidgetTrees;
using WForest.Utilities;

namespace WForest.UI.Props.Grid.Utils
{
    internal static class ApplyUtils
    {
        internal static void ApplyIfThereAreChildren(WidgetTree wt, string noApplyMsq, Action logic)
        {
            if (wt.Children.Count == 0)
                Log.Warning(noApplyMsq);
            else
                logic();
        }

        internal static bool TryExtractRows(WidgetTree widgetNode, out List<WidgetsDataSubList> rows)
        {
            var maybeRows = ExtractProp<Row>(widgetNode);
            if (maybeRows.TryGetValue(out var res))
            {
                rows = res.Rows;
                return true;
            }
            rows = new List<WidgetsDataSubList>();
            return false;
        }

        internal static bool TryExtractColumns(WidgetTree widgetNode, out List<WidgetsDataSubList> columns)
        {
            var maybeCols = ExtractProp<Column>(widgetNode);
            
            if (maybeCols.TryGetValue(out var res))
            {
                columns = res.Columns;
                return true;
            }

            columns = new List<WidgetsDataSubList>();
            return false;
        }

        internal static Maybe<Row> ExtractRowProp(WidgetTree widgetNode)
            => ExtractProp<Row>(widgetNode);

        internal static Maybe<Column> ExtractColumnProp(WidgetTree widgetNode)
            => ExtractProp<Column>(widgetNode);

        private static Maybe<T> ExtractProp<T> (WidgetTree widgetNode) where T : Prop
        {
            var prop = widgetNode.Properties.Find(p => p is T);
            return prop != null ? Maybe.Some((T) prop) : Maybe.None;
        }
    }
}