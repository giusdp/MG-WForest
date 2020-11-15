using System;
using System.Collections.Generic;
using System.Linq;
using PiBa.UI.Widgets;
using PiBa.Utilities.Collections;

namespace PiBa.UI.Properties.Grid
{
    internal class GridHandler
    {
        private static int GetWidgetWidth(Tree<Widget> t) => t.Data.Space.Size.X;
        private static int GetWidgetHeight(Tree<Widget> t) => t.Data.Space.Size.Y;


        public static (int, int) SumHeightsTilFit(List<Tree<Widget>> children, int startIdx, int maxHeight)
            => GetSizeAndIndexTilLimitSize(children, startIdx, maxHeight, GetWidgetHeight);
        
        public static (int, int) SumWidthsTilFit(List<Tree<Widget>> children, int startIdx, int maxWidth)
            => GetSizeAndIndexTilLimitSize(children, startIdx, maxWidth, GetWidgetWidth);

        public static int MaxWidthInSubList(List<Tree<Widget>> children, int from, int until) =>
            GetMaxSizeInChildrenSubList(children, from, until, GetWidgetWidth);

        public static int MaxHeightInSubList(List<Tree<Widget>> children, int from, int until) =>
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
    }
}