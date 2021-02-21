using System;
using System.Collections.Generic;
using System.Linq;
using WForest.Utilities;
using WForest.Utilities.WidgetUtils;
using WForest.Widgets.Interfaces;

namespace WForest.Props.Props.Grid.Utils
{
    internal static class GridHelper
    {
        #region API

        internal static List<WidgetsDataSubList> OrganizeWidgetsInColumns(IWidget widget)
            => OrganizeWidgetsInSubLists(widget, CreateColumn, OffsetWidgetsInColumns);

        internal static List<WidgetsDataSubList> OrganizeWidgetsInRows(IWidget widget)
            => OrganizeWidgetsInSubLists(widget, CreateRow, OffsetWidgetsInRows);

        #endregion

        #region DI Functions

        internal static float WidgetWidth(IWidget w) => w.TotalSpaceOccupied.Width;
        internal static float WidgetHeight(IWidget w) => w.TotalSpaceOccupied.Height;

        private static float SubListWidth(WidgetsDataSubList w) => w.Width;
        private static float SubListHeight(WidgetsDataSubList w) => w.Height;

        private static RectangleF AddToX(RectangleF r, float v) => new RectangleF(r.X + v, r.Y, r.Width, r.Height);
        private static RectangleF AddToY(RectangleF r, float v) => new RectangleF(r.X, r.Y + v, r.Width, r.Height);

        private static (WidgetsDataSubList, int) CreateColumn(IWidget widget, int startIdx)
        {
            var (x, firstIndexOnSubList) =
                SumHeightsTilFit(widget.Children, startIdx, WidgetHeight(widget));

            var y = MaxWidthInSubList(widget.Children, startIdx, firstIndexOnSubList);

            var subL = new WidgetsDataSubList(y, x, startIdx,
                firstIndexOnSubList < 0 ? widget.Children.Count : firstIndexOnSubList);
            return (subL, firstIndexOnSubList);
        }

        private static (WidgetsDataSubList, int) CreateRow(IWidget widget, int startIdx)
        {
            var (x, firstIndexOnSubList) =
                SumWidthsTilFit(widget.Children, startIdx, WidgetWidth(widget));

            var y = MaxHeightInSubList(widget.Children, startIdx, firstIndexOnSubList);

            var subL = new WidgetsDataSubList(x, y, startIdx,
                firstIndexOnSubList < 0 ? widget.Children.Count : firstIndexOnSubList);
            return (subL, firstIndexOnSubList);
        }

        #endregion

        #region Backend

        private static List<WidgetsDataSubList> OrganizeWidgetsInSubLists(IWidget widget,
            Func<IWidget, int, (WidgetsDataSubList, int)> create,
            Action<ICollection<IWidget>, List<WidgetsDataSubList>> offset)
        {
            var l = BuildSubLists(widget, create);
            offset(widget.Children, l);
            return l;
        }

        private static void OffsetWidgetsInRows(ICollection<IWidget> widgets, List<WidgetsDataSubList> rows)
        {
            OffsetByMainPosition(widgets, rows, WidgetWidth, AddToX);
            OffsetBySecondaryPosition(widgets, rows, SubListHeight, AddToY);
        }

        private static void OffsetWidgetsInColumns(ICollection<IWidget> widgets, List<WidgetsDataSubList> cols)
        {
            OffsetByMainPosition(widgets, cols, WidgetHeight, AddToY);
            OffsetBySecondaryPosition(widgets, cols, SubListWidth, AddToX);
        }

        private static void OffsetByMainPosition(ICollection<IWidget> widgets, List<WidgetsDataSubList> subLists,
            Func<IWidget, float> getSize, Func<RectangleF, float, RectangleF> updateRect)
        {
            if (subLists.Count == 0) return;

            subLists.ForEach(l =>
            {
                float acc = 0;
                for (var i = l.FirstWidgetIndex; i < l.LastWidgetIndex; i++)
                {
                    var widgetSpace = widgets.ElementAt(i).Space;
                    WidgetSpaceHelper.UpdateSpace(widgets.ElementAt(i), updateRect(widgetSpace, acc));
                    acc += getSize(widgets.ElementAt(i));
                }
            });
        }

        private static void OffsetBySecondaryPosition(ICollection<IWidget> widgets, List<WidgetsDataSubList> subLists,
            Func<WidgetsDataSubList, float> getSlSize, Func<RectangleF, float, RectangleF> updateRect)
        {
            if (subLists.Count <= 1) return;

            var acc = getSlSize(subLists[0]);
            for (var i = 1; i < subLists.Count; i++)
            {
                for (var j = subLists[i].FirstWidgetIndex; j < subLists[i].LastWidgetIndex; j++)
                    widgets.ElementAt(j).Space = updateRect(widgets.ElementAt(j).Space, acc);

                acc += getSlSize(subLists[i]);
            }
        }


        private static List<WidgetsDataSubList> BuildSubLists(IWidget widget,
            Func<IWidget, int, (WidgetsDataSubList, int)> f)
        {
            var subList = new List<WidgetsDataSubList>();
            var previousIndex = 0;

            var done = false;
            while (!done)
            {
                var (sl, firstIndexOnList) = f(widget, previousIndex);
                subList.Add(sl);
                previousIndex = firstIndexOnList;
                done = firstIndexOnList == -1;
                if (!done) System.Diagnostics.Debug.WriteLine("Widget violated its size limit and was broken up in 2 or more parts", "WARNING");
            }

            return subList;
        }

        private static (float, int) SumHeightsTilFit(ICollection<IWidget> children, int startIdx, float maxHeight)
            => GetSizeAndIndexTilLimitSize(children, startIdx, maxHeight, WidgetHeight);

        private static (float, int) SumWidthsTilFit(ICollection<IWidget> children, int startIdx, float maxWidth)
            => GetSizeAndIndexTilLimitSize(children, startIdx, maxWidth, WidgetWidth);

        private static float MaxWidthInSubList(ICollection<IWidget> children, int from, int until) =>
            GetMaxSizeInChildrenSubList(children, from, until, WidgetWidth);

        private static float MaxHeightInSubList(ICollection<IWidget> children, int from, int until) =>
            GetMaxSizeInChildrenSubList(children, from, until, WidgetHeight);

        private static float GetMaxSizeInChildrenSubList(ICollection<IWidget> cs, int from, int until,
            Func<IWidget, float> size)
            => cs.ToList().GetRange(from, until <= 0 ? cs.Count - from : until - from).Max(size);


        private static (float, int) GetSizeAndIndexTilLimitSize(ICollection<IWidget> children, int firstChildIndex,
            float limit,
            Func<IWidget, float> getSize)
        {
            float acc = 0;
            var indexOnNewRow = -1;

            for (var i = firstChildIndex; i < children.Count; i++)
            {
                var size = getSize(children.ElementAt(i));
                if (acc + size > limit && size < limit)
                {
                    indexOnNewRow = i;
                    break;
                }

                acc += size;
            }

            return (acc, indexOnNewRow);
        }

        #endregion
    }
}