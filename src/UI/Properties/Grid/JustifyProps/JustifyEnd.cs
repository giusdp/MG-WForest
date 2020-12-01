using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WForest.UI.Properties.Grid.Utils;
using WForest.UI.Utils;
using WForest.UI.Widgets;
using WForest.Utilities.Collections;

namespace WForest.UI.Properties.Grid.JustifyProps
{
    public class JustifyEnd : Property
    {
        internal override int Priority { get; } = 2;

        internal override void ApplyOn(WidgetTree widgetNode)
        {
            ApplyUtils.ApplyIfThereAreChildren(widgetNode,
                $"{widgetNode.Data} has no children to justify-end.",
                () =>
                {
                    var rowsAtEnd =
                        PutAtEnd(widgetNode, GridHelper.WidgetWidth, (x, c) => new Point(x, c.Data.Space.Y),
                            c => c.Margin.Left);
                    var colsAtEnd =
                        PutAtEnd(widgetNode, GridHelper.WidgetHeight, (y, c) => new Point(c.Data.Space.X, y),
                            c => c.Margin.Top);

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
                        WidgetsSpaceHelper.UpdateSpace(child,new Rectangle(updateLoc(acc, child),
                            child.Data.Space.Size));
                        acc -= getMargin(child.Data);
                    }
                });
        }
    }
}