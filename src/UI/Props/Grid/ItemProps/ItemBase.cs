using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WForest.UI.Props.Grid.Utils;
using WForest.UI.Utils;
using WForest.UI.Widgets;
using WForest.UI.WidgetTrees;
using WForest.Utilities.Collections;

namespace WForest.UI.Props.Grid.ItemProps
{
    /// <summary>
    /// Property to move widgets to the bottom in a Row or to the right in a Column.
    /// Differently from JustifyEnd, this property deals with the opposite axis of a Row/Column.
    /// </summary>
    public class ItemBase : Prop
    {
        internal ItemBase()
        {
        }

        /// <summary>
        /// Since it changes the layout for the other axis in a Row or Col, it should be applied after the layout
        /// for the main axis is applied.
        /// Row/Col have priority of 1, the Justify properties have priority of 2, so this has priority of 3.
        /// </summary>
        public override int Priority { get; } = 3;

        /// <summary>
        /// Move the widgets to the bottom of a Row or right of a Column.
        /// </summary>
        /// <param name="widgetNode"></param>
        public override void ApplyOn(WidgetTree widgetNode)
        {
            ApplyUtils.ApplyIfThereAreChildren(widgetNode, $"{widgetNode.Data} has no children to item-base.",
                () =>
                {
                    var rowsAtBase =
                        PutAtBase(widgetNode, l => l.Height, GridHelper.WidgetHeight,
                            (y, c) => new Point(c.Data.Space.X, y + c.Data.MarginValues.Top));
                    var colsAtBase =
                        PutAtBase(widgetNode, l => l.Width, GridHelper.WidgetWidth,
                            (x, c) => new Point(x + c.Data.MarginValues.Left, c.Data.Space.Y));

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
                        WidgetsSpaceHelper.UpdateSpace(child, new Rectangle(updateLoc(newCoord, child),
                            child.Data.Space.Size));
                    }

                    acc -= listSize(list);
                }
            };
        }
    }
}