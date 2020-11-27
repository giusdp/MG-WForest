using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using WForest.UI.Properties.Grid.Utils;
using WForest.UI.Widgets;
using WForest.Utilities.Collections;
using static WForest.UI.Properties.Grid.Utils.GridHelper;

namespace WForest.UI.Properties.Grid.JustifyProps
{
    public class JustifySpaceBetween : IProperty
    {
        public int Priority { get; } = 2;

        public void ApplyOn(WidgetTree widgetNode)
        {
            ApplyUtils.ApplyIfThereAreChildren(widgetNode,
                $"{widgetNode.Data} has no children to justify space between.",
                () =>
                {
                    if (widgetNode.Children.Count == 1) return;
                    if (ApplyUtils.TryExtractRows(widgetNode, out var rows))
                        SpaceBetweenHorizontally(widgetNode, rows);
                    else if (ApplyUtils.TryExtractColumns(widgetNode, out var cols))
                        SpaceBetweenVertically(widgetNode, cols);
                });
        }

        private static void SpaceBetweenHorizontally(WidgetTree wTree, List<WidgetsDataSubList> lists)
        {
            int start = wTree.Data.TotalSpaceOccupied.X;
            int size = WidgetWidth(wTree);
            lists.ForEach(r =>
                DivideSpaceEvenly(start, size,
                    wTree.Children.GetRange(r.FirstWidgetIndex, r.LastWidgetIndex - r.FirstWidgetIndex ),
                    WidgetWidth,
                    (c, p) => new Point(p, c.TotalSpaceOccupied.Y))
            );
        }

        private static void SpaceBetweenVertically(WidgetTree wTree, List<WidgetsDataSubList> lists)
        {
            int start = wTree.Data.TotalSpaceOccupied.Y;
            int size = WidgetHeight(wTree);
            lists.ForEach(r =>
                DivideSpaceEvenly(start, size,
                    wTree.Children.GetRange(r.FirstWidgetIndex, r.LastWidgetIndex - r.FirstWidgetIndex ),
                    WidgetHeight,
                    (c, pos) => new Point(c.TotalSpaceOccupied.X, pos))
            );
        }

        private static void DivideSpaceEvenly(int start, int parentSize, List<Tree<Widget>> widgets,
            Func<Tree<Widget>, int> getSize, Func<Widget, int, Point> updateLoc)
        {
            float startPoint = start;
            float usedPixels = widgets.Sum(getSize);
            float freePixels = parentSize - usedPixels;
            float spaceBetween = freePixels / (widgets.Count - 1.0f);

            widgets.ForEach(w =>
            {
                ((WidgetTree) w).UpdateSpace(new Rectangle(updateLoc(w.Data, start), w.Data.Space.Size));
                startPoint += getSize(w) + spaceBetween;
                start = (int) Math.Round(startPoint);
            });
        }
    }
}