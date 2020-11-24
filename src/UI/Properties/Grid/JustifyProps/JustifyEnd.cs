using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Serilog;
using WForest.UI.Properties.Grid.Utils;
using WForest.UI.Widgets;
using WForest.Utilities.Collections;

namespace WForest.UI.Properties.Grid.JustifyProps
{
    public class JustifyEnd : IProperty
    {
        public int Priority { get; } = 2;

        public void ApplyOn(WidgetTree widgetNode)
        {
            
            if (widgetNode.Children.Count == 0)
            {
                Log.Warning($"{widgetNode.Data} has no children to justify-end.");
                return;
            }

            var rowsAtEnd =
                PutAtEnd(widgetNode, GridHelper.WidgetWidth, (x, c) => new Point(x, c.Data.Space.Y));
            var colsAtEnd =
                PutAtEnd(widgetNode, GridHelper.WidgetHeight, (y, c) => new Point(c.Data.Space.X, y));
            
            if (GridHelper.TryExtractRows(widgetNode, out var rows))
                rowsAtEnd(rows);
            else if (GridHelper.TryExtractColumns(widgetNode, out var cols))
                colsAtEnd(cols);
        }

        private static Action<List<WidgetsDataSubList>> PutAtEnd(WidgetTree wTree, Func<Tree<Widget>, int> getSize,
            Func<int, Tree<Widget>, Point> updateLoc)
        {
            return wLists =>
                wLists.ForEach(r =>
                {
                    var acc = getSize(wTree);
                    for (var i = r.LastWidgetIndex-1; i >= r.FirstWidgetIndex; i--)
                    {
                        var child = wTree.Children[i];
                        acc -= getSize(child);
                        ((WidgetTree) child).UpdateSpace(new Rectangle(updateLoc(acc, child),
                            child.Data.Space.Size));
                    }
                });
        }
    }
}