using System;
using System.Collections.Generic;
using Serilog;
using WForest.UI.WidgetTrees;

namespace WForest.UI.Properties.Grid.Utils
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
            var b = TryExtractProp<Row.Row>(widgetNode, out var res);
            rows = b ? res.Rows : new List<WidgetsDataSubList>();
            return b;
        }

        internal static bool TryExtractColumns(WidgetTree widgetNode, out List<WidgetsDataSubList> columns)
        {
            var b = TryExtractProp<Column.Column>(widgetNode, out var res);
            columns = b ? res.Columns : new List<WidgetsDataSubList>();
            return b;
        }

        internal static bool TryExtractRow(WidgetTree widgetNode, out Row.Row rowProp)
            => TryExtractProp(widgetNode, out rowProp);

        internal static bool TryExtractColumn(WidgetTree widgetNode, out Column.Column colProp)
            => TryExtractProp(widgetNode, out colProp);

        private static bool TryExtractProp<T> (WidgetTree widgetNode, out T res) where T : Property
        {
            var prop = widgetNode.Properties.Find(p => p is T);
            if (prop != null)
            {
                res = (T) prop;
                return true;
            }
            res = default;
            return false;
        }
    }
}