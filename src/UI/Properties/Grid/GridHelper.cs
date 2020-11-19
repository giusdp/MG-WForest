using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using WForest.UI.Widgets;
using WForest.Utilities.Collections;

namespace WForest.UI.Properties.Grid
{
    internal static class GridHelper
    {
        #region Public API

        public static List<WidgetsDataSubList> OrganizeWidgetsInColumns(WidgetTree widget)
            => OrganizeWidgetsInSubLists(widget, CreateColumn, OffsetWidgetsInColumns);

        public static List<WidgetsDataSubList> OrganizeWidgetsInRows(WidgetTree widgetTree)
            => OrganizeWidgetsInSubLists(widgetTree, CreateRow, OffsetWidgetsInRows);

        #endregion

        #region DI Functions

        private static int WidgetWidth(Tree<Widget> t) => t.Data.TotalSpaceOccupied.Width;
        private static int WidgetHeight(Tree<Widget> t) => t.Data.TotalSpaceOccupied.Height;

        private static int SubListWidth(WidgetsDataSubList w) => w.Width;
        private static int SubListHeight(WidgetsDataSubList w) => w.Height;

        private static Rectangle AddToX(Rectangle r, int v) => new Rectangle(r.X + v, r.Y, r.Width, r.Height);
        private static Rectangle AddToY(Rectangle r, int v) => new Rectangle(r.X, r.Y + v, r.Width, r.Height);

        private static (WidgetsDataSubList, int) CreateColumn(WidgetTree widget, int startIdx)
        {
            var (x, firstIndexOnSubList) =
                SumHeightsTilFit(widget.Children, startIdx, WidgetHeight(widget));

            var y = MaxWidthInSubList(widget.Children, startIdx, firstIndexOnSubList);

            var subL = new WidgetsDataSubList(y, x, startIdx,
                firstIndexOnSubList < 0 ? widget.Children.Count : firstIndexOnSubList);
            return (subL, firstIndexOnSubList);
        }

        private static (WidgetsDataSubList, int) CreateRow(WidgetTree widget, int startIdx)
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

        private static List<WidgetsDataSubList> OrganizeWidgetsInSubLists(WidgetTree widget,
            Func<WidgetTree, int, (WidgetsDataSubList, int)> f,
            Action<List<Tree<Widget>>, List<WidgetsDataSubList>> offset)
        {
            var l = BuildSubLists(widget, f);
            offset(widget.Children, l);
            return l;
        }

        private static void OffsetWidgetsInRows(List<Tree<Widget>> widgetTrees, List<WidgetsDataSubList> rows)
        {
            OffsetByMainPosition(widgetTrees, rows, WidgetWidth, AddToX);
            OffsetBySecondaryPosition(widgetTrees, rows, SubListHeight, AddToY);
        }

        private static void OffsetWidgetsInColumns(List<Tree<Widget>> widgetTrees, List<WidgetsDataSubList> cols)
        {
            OffsetByMainPosition(widgetTrees, cols, WidgetHeight, AddToY);
            OffsetBySecondaryPosition(widgetTrees, cols, SubListWidth, AddToX);
        }

        private static void OffsetByMainPosition(List<Tree<Widget>> widgetTrees, List<WidgetsDataSubList> subLists,
            Func<Tree<Widget>, int> getSize, Func<Rectangle, int, Rectangle> updateRect)
        {
            if (subLists.Count == 0) return;

            subLists.ForEach(l =>
            {
                var acc = 0;
                for (var i = l.FirstWidgetIndex; i < l.LastWidgetIndex; i++)
                {
                    var widgetSpace = widgetTrees[i].Data.Space;
                    widgetTrees[i].Data.Space = updateRect(widgetSpace, acc);
                    acc += getSize(widgetTrees[i]);
                }
            });
        }

        private static void OffsetBySecondaryPosition(List<Tree<Widget>> widgetTrees, List<WidgetsDataSubList> subLists,
            Func<WidgetsDataSubList, int> getSlSize, Func<Rectangle, int, Rectangle> updateRect)
        {
            if (subLists.Count <= 1) return;

            var widgets = widgetTrees.Select(t => t.Data).ToList();
            var acc = getSlSize(subLists[0]);
            for (var i = 1; i < subLists.Count; i++)
            {
                for (var j = subLists[i].FirstWidgetIndex; j < subLists[i].LastWidgetIndex; j++)
                    widgets[j].Space = updateRect(widgets[j].Space, acc);

                acc += getSlSize(subLists[i]);
            }
        }


        private static List<WidgetsDataSubList> BuildSubLists(WidgetTree widget,
            Func<WidgetTree, int, (WidgetsDataSubList, int)> f)
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
            }

            return subList;
        }

        private static (int, int) SumHeightsTilFit(List<Tree<Widget>> children, int startIdx, int maxHeight)
            => GetSizeAndIndexTilLimitSize(children, startIdx, maxHeight, WidgetHeight);

        private static (int, int) SumWidthsTilFit(List<Tree<Widget>> children, int startIdx, int maxWidth)
            => GetSizeAndIndexTilLimitSize(children, startIdx, maxWidth, WidgetWidth);

        private static int MaxWidthInSubList(List<Tree<Widget>> children, int from, int until) =>
            GetMaxSizeInChildrenSubList(children, from, until, WidgetWidth);

        private static int MaxHeightInSubList(List<Tree<Widget>> children, int from, int until) =>
            GetMaxSizeInChildrenSubList(children, from, until, WidgetHeight);

        private static int GetMaxSizeInChildrenSubList(List<Tree<Widget>> children, int from, int until,
            Func<Tree<Widget>, int> getSize) =>
            children.GetRange(from, until < 0 ? children.Count - from : until - from).Max(getSize);

        private static (int, int) GetSizeAndIndexTilLimitSize(List<Tree<Widget>> children, int firstChildIndex,
            int limit,
            Func<Tree<Widget>, int> getSize)
        {
            var acc = 0;
            var indexOnNewRow = -1;

            for (var i = firstChildIndex; i < children.Count; i++)
            {
                var size = getSize(children[i]);
                if (acc + size > limit)
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