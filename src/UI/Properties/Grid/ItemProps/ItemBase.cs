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
                        PutAtBase(widgetNode, l => l.Height, GridHelper.WidgetHeight,
                            (y, c) => new Point(c.Data.Space.X, y + c.Data.Margin.Top));
                    var colsAtBase =
                        PutAtBase(widgetNode, l => l.Width, GridHelper.WidgetWidth,
                            (x, c) => new Point(x + c.Data.Margin.Left, c.Data.Space.Y));

                    if (ApplyUtils.TryExtractRows(widgetNode, out var rows))
                        rowsAtBase(rows);
                    else if (ApplyUtils.TryExtractColumns(widgetNode, out var cols))
                        colsAtBase(cols);
                });
        }

        private static Action<List<WidgetsDataSubList>> PutAtBase(WidgetTree wTree,
            Func<WidgetsDataSubList, int> listSize, Func<Tree<Widget>, int> wSize,
            Func<int, Tree<Widget>, Point> updateLoc)
        {
            return wLists =>
            {
                var acc = wSize(wTree);
                for (var i = wLists.Count - 1; i >= 0; i--)
                {
                    var list = wLists[i];
                    for (var j = list.FirstWidgetIndex; j < list.LastWidgetIndex; j++)
                    {
                        var child = wTree.Children[j];
                        var newCoord = acc - wSize(child); 
                        ((WidgetTree) child).UpdateSpace(new Rectangle(updateLoc(newCoord, child),
                            child.Data.Space.Size));
                    }

                    acc -= listSize(list);
                }
            };
        }
    }
}