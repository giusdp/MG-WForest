using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WForest.UI.Props.Grid.Utils;
using WForest.UI.Utils;
using WForest.UI.Widgets;
using WForest.UI.WidgetTrees;
using WForest.Utilities.Collections;

namespace WForest.UI.Props.Grid.JustifyProps
{
    /// <summary>
    /// Property to move widgets to the end of a Row or Column.
    /// </summary>
    public class JustifyEnd : Prop
    {
        /// <summary>
        /// Since it changes the layout internally in a Row or Col, it should be applied after them.
        /// Row/Col have priority of 1 so this has priority of 2.
        /// </summary>
        public override int Priority { get; } = 2;

        internal JustifyEnd()
        {
        }

        /// <summary>
        /// Move the widgets to the end of a Row (to the right) or of a Col (to the bottom).
        /// </summary>
        /// <param name="widgetNode"></param>
        public override void ApplyOn(WidgetTree widgetNode)
        {
            ApplyUtils.ApplyIfThereAreChildren(widgetNode,
                $"{widgetNode.Data} has no children to justify-end.",
                () =>
                {
                    var rowsAtEnd =
                        PutAtEnd(widgetNode, GridHelper.WidgetWidth, (x, c) => new Point(x, c.Data.Space.Y),
                            c => c.MarginValues.Left);
                    var colsAtEnd =
                        PutAtEnd(widgetNode, GridHelper.WidgetHeight, (y, c) => new Point(c.Data.Space.X, y),
                            c => c.MarginValues.Top);

                    if (ApplyUtils.TryExtractRows(widgetNode, out var rows))
                        rowsAtEnd(rows);
                    else if (ApplyUtils.TryExtractColumns(widgetNode, out var cols))
                        colsAtEnd(cols);
                });
        }

        private static Action<List<WidgetsDataSubList>> PutAtEnd(WidgetTree wTree, Func<Tree<Widget>, int> getSize,
            Func<int, Tree<Widget>, Point> updateLoc, Func<Widget, int> getMargin)
        {
            return wLists =>
                wLists.ForEach(r =>
                {
                    var acc = getSize(wTree);
                    for (var i = r.LastWidgetIndex - 1; i >= r.FirstWidgetIndex; i--)
                    {
                        var child = wTree.Children[i];
                        acc -= getSize(child) - getMargin(child.Data);
                        WidgetsSpaceHelper.UpdateSpace(child, new Rectangle(updateLoc(acc, child),
                            child.Data.Space.Size));
                        acc -= getMargin(child.Data);
                    }
                });
        }
    }
}