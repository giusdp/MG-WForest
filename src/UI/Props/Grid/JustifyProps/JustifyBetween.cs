using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using WForest.UI.Props.Grid.Utils;
using WForest.UI.Utils;
using WForest.UI.Widgets;
using WForest.UI.WidgetTrees;
using WForest.Utilities.Collections;
using static WForest.UI.Props.Grid.Utils.GridHelper;

namespace WForest.UI.Props.Grid.JustifyProps
{
    /// <summary>
    /// Property to separate widgets in a Row or Column, setting maximum space in between them.
    /// </summary>
    public class JustifyBetween : Prop
    {
        /// <summary>
        /// Since it changes the layout internally in a Row or Col, it should be applied after them.
        /// Row/Col have priority of 1 so this has priority of 2.
        /// </summary>
        public override int Priority { get; } = 2;

        internal JustifyBetween()
        {
        }

        /// <summary>
        /// Move the widgets in a way to have them maximally separated between them.
        /// In a Row they are separated horizontally, in a Column vertically.
        /// </summary>
        /// <param name="widgetNode"></param>
        public override void ApplyOn(WidgetTree widgetNode)
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
            int start = wTree.Data.Space.X;
            int size = WidgetWidth(wTree);
            lists.ForEach(r =>
                DivideSpaceEvenly(start, size,
                    wTree.Children.GetRange(r.FirstWidgetIndex, r.LastWidgetIndex - r.FirstWidgetIndex),
                    WidgetWidth,
                    (c, p) => new Point(p + c.MarginValues.Left, c.Space.Y))
            );
        }

        private static void SpaceBetweenVertically(WidgetTree wTree, List<WidgetsDataSubList> lists)
        {
            int start = wTree.Data.Space.Y;
            int size = WidgetHeight(wTree);
            lists.ForEach(r =>
                DivideSpaceEvenly(start, size,
                    wTree.Children.GetRange(r.FirstWidgetIndex, r.LastWidgetIndex - r.FirstWidgetIndex),
                    WidgetHeight,
                    (c, pos) => new Point(c.Space.X, pos + c.MarginValues.Top))
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
                WidgetsSpaceHelper.UpdateSpace(w, new Rectangle(updateLoc(w.Data, start), w.Data.Space.Size));
                startPoint += getSize(w) + spaceBetween;
                start = (int) Math.Round(startPoint);
            });
        }
    }
}