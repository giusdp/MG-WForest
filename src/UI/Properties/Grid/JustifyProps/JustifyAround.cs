using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using WForest.UI.Properties.Grid.Utils;
using WForest.UI.Utils;
using WForest.UI.Widgets;
using WForest.UI.WidgetTrees;
using WForest.Utilities.Collections;
using static WForest.UI.Properties.Grid.Utils.GridHelper;

namespace WForest.UI.Properties.Grid.JustifyProps
{
    public class JustifyAround : Property
    {
        internal override int Priority { get; } = 2;

        internal JustifyAround(){}
        internal override void ApplyOn(WidgetTree widgetNode)
        {
            ApplyUtils.ApplyIfThereAreChildren(widgetNode,
                $"{widgetNode.Data} has no children to justify space between.",
                () =>
                {
                    if (widgetNode.Children.Count == 1) return;
                    if (ApplyUtils.TryExtractRows(widgetNode, out var rows))
                        SpaceAroundHorizontally(widgetNode, rows);
                    else if (ApplyUtils.TryExtractColumns(widgetNode, out var cols))
                        SpaceAroundVertically(widgetNode, cols);
                });
        }

        private static void SpaceAroundHorizontally(WidgetTree wTree, List<WidgetsDataSubList> lists)
        {
            int start = wTree.Data.Space.X;
            int size = WidgetWidth(wTree);
            lists.ForEach(r =>
                DivideSpaceEvenly(start, size,
                    wTree.Children.GetRange(r.FirstWidgetIndex, r.LastWidgetIndex - r.FirstWidgetIndex),
                    WidgetWidth,
                    (c, p) => new Point(p + c.Margin.Left, c.Space.Y))
            );
        }

        private static void SpaceAroundVertically(WidgetTree wTree, List<WidgetsDataSubList> lists)
        {
            int start = wTree.Data.Space.Y;
            int size = WidgetHeight(wTree);
            lists.ForEach(r =>
                DivideSpaceEvenly(start, size,
                    wTree.Children.GetRange(r.FirstWidgetIndex, r.LastWidgetIndex - r.FirstWidgetIndex),
                    WidgetHeight,
                    (c, p) => new Point(c.Space.X, p + c.Margin.Top))
            );
        }

        private static void DivideSpaceEvenly(int start, int parentSize, List<Tree<Widget>> widgets,
            Func<Tree<Widget>, int> getSize, Func<Widget, int, Point> updateLoc)
        {
            float usedPixels = widgets.Sum(getSize);
            float freePixels = parentSize - usedPixels;
            float spaceBetween = freePixels / (widgets.Count + 1.0f);
            float startPoint = start + spaceBetween;
            start += (int) Math.Round(spaceBetween);

            widgets.ForEach(w =>
            {
                WidgetsSpaceHelper.UpdateSpace(w,new Rectangle(updateLoc(w.Data, start), w.Data.Space.Size));
                startPoint += getSize(w) + spaceBetween;
                start = (int) Math.Round(startPoint);
            });
        }
    }
}