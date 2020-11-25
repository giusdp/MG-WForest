using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WForest.UI.Properties.Grid.Utils;
using WForest.UI.Widgets;
using WForest.Utilities.Collections;

namespace WForest.UI.Properties.Grid.ItemProps
{
    public class ItemBase : IProperty
    {
        public int Priority { get; } = 3;

        public void ApplyOn(WidgetTree widgetNode)
        {
            ApplyUtils.ApplyIfThereAreChildren(widgetNode, $"{widgetNode.Data} has no children to item-base.",
                () =>
                {
                    var rowsAtBase =
                        PutAtBase(widgetNode, GridHelper.WidgetHeight,
                            (y, c) => new Point(c.Data.Space.X, y));
                    var colsAtBase =
                        PutAtBase(widgetNode, GridHelper.WidgetWidth, (x, c) => new Point(x, c.Data.Space.Y));

                    if (ApplyUtils.TryExtractRows(widgetNode, out var rows))
                        rowsAtBase(rows);
                    else if (ApplyUtils.TryExtractColumns(widgetNode, out var cols))
                        colsAtBase(cols);
                });
        }

        private static Action<List<WidgetsDataSubList>> PutAtBase(WidgetTree wTree, Func<Tree<Widget>, int> getSize,
            Func<int, Tree<Widget>, Point> updateLoc)
        {
            return wLists =>
                wLists.ForEach(r =>
                {
                    var baseCoord = getSize(wTree);
                    for (var i = r.FirstWidgetIndex; i < r.LastWidgetIndex; i++)
                    {
                        var child = wTree.Children[i];
                        var newCoord = baseCoord - getSize(child);
                        ((WidgetTree) child).UpdateSpace(new Rectangle(updateLoc(newCoord, child),
                            child.Data.Space.Size));
                    }
                });
        }
    }
}