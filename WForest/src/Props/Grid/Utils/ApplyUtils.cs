using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;
using WForest.Props.Grid.StretchingProps;
using WForest.Props.Interfaces;
using WForest.Utilities;
using WForest.Widgets.Interfaces;

namespace WForest.Props.Grid.Utils
{
    internal static class ApplyUtils
    {
        internal static (float, List<IWidget>) StretchedWidthUsingWidthSiblings(IWidget widget)
            => GetStretchedSizeWithToBeProcessedSiblings<Row, HorizontalStretch, VerticalStretch>(widget,
                GridHelper.WidgetWidth);

        internal static (float, List<IWidget>) StretchedHeightUsingHeightSiblings(IWidget widget)
            => GetStretchedSizeWithToBeProcessedSiblings<Column, VerticalStretch, HorizontalStretch>(widget,
                GridHelper.WidgetHeight);

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

        private static (float, List<IWidget>) GetStretchedSizeWithToBeProcessedSiblings<T, TV, TH>(IWidget widget,
            Func<IWidget, float> getSize) where T : IProp where TV : IProp where TH : IProp
        {
            List<IWidget> nonFinishedSiblingsWidth = new();
            var size = getSize(widget.Parent!);
            if (!widget.Parent!.Props.SafeGetByProp<T>().TryGetValue(out var grid))
                return (size, nonFinishedSiblingsWidth);
            if (grid!.FirstOrDefault() == null) return (size, nonFinishedSiblingsWidth);
            var siblings = widget.Parent.Children.Where(w => w != widget);
            var count = 1;
            float nonStretchedSize = 0;

            foreach (var sibling in siblings)
            {
                if (sibling.Props.Contains<TV>()) count++;
                else
                {
                    if (!sibling.Props.Contains<TH>())
                        nonStretchedSize += getSize(sibling);
                    else if (sibling.Props.GetByProp<TH>().FirstOrDefault() is IApplicableProp hp)
                    {
                        if (hp.ApplicationDone) nonStretchedSize += getSize(sibling);
                        else nonFinishedSiblingsWidth.Add(sibling);
                    }
                    else nonStretchedSize += getSize(sibling);
                }
            }

            size -= nonStretchedSize;
            return (size / count, nonFinishedSiblingsWidth);
        }
    }
}