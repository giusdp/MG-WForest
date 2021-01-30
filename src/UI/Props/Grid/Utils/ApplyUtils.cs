using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;
using WForest.UI.Props.Interfaces;
using WForest.UI.Widgets.Interfaces;
using WForest.Utilities;

namespace WForest.UI.Props.Grid.Utils
{
    internal static class ApplyUtils
    {
        internal static void ApplyIfThereAreChildren(IWidget wt, string noApplyMsq, Action logic)
        {
            if (wt.Children.Count == 0)
                Log.Warning("{WarnMessage}", noApplyMsq);
            else
                logic();
        }

        internal static bool TryExtractRows(IWidget widgetNode, out List<WidgetsDataSubList> rows)
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

        internal static bool TryExtractColumns(IWidget widgetNode, out List<WidgetsDataSubList> columns)
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

        internal static Maybe<Row> ExtractRowProp(IWidget widgetNode)
            => ExtractProp<Row>(widgetNode);

        internal static Maybe<Column> ExtractColumnProp(IWidget widgetNode)
            => ExtractProp<Column>(widgetNode);

        private static Maybe<T> ExtractProp<T>(IPropHolder widgetNode) where T : IProp
        {
            return widgetNode.Props.SafeGetByProp<T>().Bind(l => l.Any() ? Maybe.Some((T) l.First()) : Maybe.None);
        }
    }
}