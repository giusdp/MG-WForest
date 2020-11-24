using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;

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
            => TryExtract<Row.Row>(widgetNode, out rows, r => r.Rows);

        internal static bool TryExtractColumns(WidgetTree widgetNode, out List<WidgetsDataSubList> columns)
            => TryExtract<Column.Column>(widgetNode, out columns, c => c.Columns);

        private static bool TryExtract<T>(WidgetTree widgetNode, out List<WidgetsDataSubList> rows,
            Func<T, List<WidgetsDataSubList>> extractor)
        {
            var props = widgetNode.Properties.OfType<T>().ToList();
            if (props.Any())
            {
                rows = extractor(props.First());
                return true;
            }

            rows = new List<WidgetsDataSubList>();
            return false;
        }
    }
}