using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using PiBa.UI.Widgets;
using PiBa.Utilities.Collections;

namespace PiBa.UI.Properties.Grid
{
    internal static class GridHandler
    {
        #region Public API

        public static List<WidgetsDataSubList> OrganizeWidgetsInColumns(WidgetTree widget)
            => OrganizeWidgetsInSubLists(widget, CreateColumn, OffsetWidgetsInColumns);

        public static List<WidgetsDataSubList> OrganizeWidgetsInRows(WidgetTree widgetTree)
            => OrganizeWidgetsInSubLists(widgetTree, CreateRow, OffsetWidgetsInRows);

        #endregion

        #region DI Functions

        private static int GetWidgetWidth(Tree<Widget> t) => t.Data.Space.Width;
        private static int GetWidgetHeight(Tree<Widget> t) => t.Data.Space.Height;

        private static int GetSubListWidth(WidgetsDataSubList w) => w.Width;
        private static int GetSubListHeight(WidgetsDataSubList w) => w.Height;

        private static Rectangle AddToX(Rectangle r, int v) => new Rectangle(r.X + v, r.Y, r.Width, r.Height);
        private static Rectangle AddToY(Rectangle r, int v) => new Rectangle(r.X, r.Y + v, r.Width, r.Height);

        private static (WidgetsDataSubList, int) CreateColumn(WidgetTree widget, int startIdx)
        {
            var (x, firstIndexOnSubList) =
                SumHeightsTilFit(widget.Children, startIdx, GetWidgetHeight(widget));

            var y = MaxWidthInSubList(widget.Children, startIdx, firstIndexOnSubList);

            var subL = new WidgetsDataSubList(y, x, startIdx,
                firstIndexOnSubList < 0 ? widget.Children.Count : firstIndexOnSubList);
            return (subL, firstIndexOnSubList);
        }

        private static (WidgetsDataSubList, int) CreateRow(WidgetTree widget, int startIdx)
        {
            var (x, firstIndexOnSubList) =
                SumWidthsTilFit(widget.Children, startIdx, GetWidgetWidth(widget));

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
            OffsetByMainPosition(widgetTrees, rows, GetWidgetWidth, AddToX);
            OffsetBySecondaryPosition(widgetTrees, rows, GetSubListHeight, AddToY);
        }

        private static void OffsetWidgetsInColumns(List<Tree<Widget>> widgetTrees, List<WidgetsDataSubList> cols)
        {
            OffsetByMainPosition(widgetTrees, cols, GetWidgetHeight, AddToY);
            OffsetBySecondaryPosition(widgetTrees, cols, GetSubListWidth, AddToX);
        }

        private static void OffsetByMainPosition(List<Tree<Widget>> widgetTrees, List<WidgetsDataSubList> subLists,
            Func<Tree<Widget>, int> getSize, Func<Rectangle, int, Rectangle> updateRect)
        {
            if (subLists.Count == 0) return;

            subLists.ForEach(l =>
            {
                var acc = getSize(widgetTrees[l.FirstWidgetIndex]);
                for (var i = l.FirstWidgetIndex + 1; i < l.LastWidgetIndex; i++)
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
                {
                    var (x, y, width, height) = widgets[j].Space;
                    widgets[j].Space = updateRect(widgets[j].Space, acc);
                }

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
            => GetSizeAndIndexTilLimitSize(children, startIdx, maxHeight, GetWidgetHeight);

        private static (int, int) SumWidthsTilFit(List<Tree<Widget>> children, int startIdx, int maxWidth)
            => GetSizeAndIndexTilLimitSize(children, startIdx, maxWidth, GetWidgetWidth);

        private static int MaxWidthInSubList(List<Tree<Widget>> children, int from, int until) =>
            GetMaxSizeInChildrenSubList(children, from, until, GetWidgetWidth);

        private static int MaxHeightInSubList(List<Tree<Widget>> children, int from, int until) =>
            GetMaxSizeInChildrenSubList(children, from, until, GetWidgetHeight);

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