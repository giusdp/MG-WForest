using System;
using System.Collections.Generic;
using PiBa.UI.Widgets;
using PiBa.Utilities.Collections;

namespace PiBa.UI.Properties.Grid
{
    internal class GridHandler
    {
        private static int GetWidgetWidth(Tree<Widget> t) => t.Data.Space.Size.X;
        private static int GetWidgetHeight(Tree<Widget> t) => t.Data.Space.Size.Y;

        private static (int, int) GetChildrenIndexTilFitSize(List<Tree<Widget>> children, int firstChildIndex,
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

        public static (int, int) SumChildrenHeightsTilFit(List<Tree<Widget>> children, int startIdx,
            int maxHeight)
            => GetChildrenIndexTilFitSize(children, startIdx, maxHeight, GetWidgetHeight);


        public static (int, int) SumChildrenWidthsTilFit(List<Tree<Widget>> children, int startIdx, int maxWidth)
            => GetChildrenIndexTilFitSize(children, startIdx, maxWidth, GetWidgetWidth);
    }
}