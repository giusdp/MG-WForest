using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Serilog;
using WForest.Exceptions;
using WForest.UI.Props.Grid.Utils;
using WForest.UI.Props.Interfaces;
using WForest.UI.Utils;
using WForest.UI.Widgets.Interfaces;

namespace WForest.UI.Props.Grid.ItemProps
{
    /// <summary>
    /// Property to move widgets to the bottom in a Row or to the right in a Column.
    /// Differently from JustifyEnd, this property deals with the opposite axis of a Row/Column.
    /// </summary>
    public class ItemBase : IApplicableProp
    {
        internal ItemBase()
        {
        }

        /// <summary>
        /// Since it changes the layout for the other axis in a Row or Col, it should be applied after the layout
        /// for the main axis is applied.
        /// Row/Col have priority of 1, the Justify properties have priority of 2, so this has priority of 3.
        /// </summary>
        public int Priority { get; set; } = 3;

        public event EventHandler? Applied;

        /// <summary>
        /// Move the widgets to the bottom of a Row or right of a Column.
        /// </summary>
        /// <param name="widget"></param>
        public void ApplyOn(IWidget widget)
        {
            ApplyUtils.ApplyIfThereAreChildren(widget, $"{widget} has no children to item-base.",
                () =>
                {
                    var rowsAtBase =
                        PutAtBase(widget, l => l.Height, GridHelper.WidgetHeight,
                            (y, c) => new Point(c.Space.X, y + c.Margins.Top));
                    var colsAtBase =
                        PutAtBase(widget, l => l.Width, GridHelper.WidgetWidth,
                            (x, c) => new Point(x + c.Margins.Left, c.Space.Y));

                    if (ApplyUtils.TryExtractRows(widget, out var rows))
                        rowsAtBase(rows);
                    else if (ApplyUtils.TryExtractColumns(widget, out var cols))
                        colsAtBase(cols);
                    else
                    {
                        Log.Error(
                            "ItemBase can only be applied to a Row or Column Widget! Make sure this {W} has a Row or Column Prop",
                            widget);
                        throw new IncompatibleWidgetException(
                            "Tried to apply ItemBase to a widget without a Row or Column Prop");
                    }
                });
            OnApplied();
        }

        protected virtual void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);

        private static Action<List<WidgetsDataSubList>> PutAtBase(IWidget wTree,
            Func<WidgetsDataSubList, int> listSize, Func<IWidget, int> wSize,
            Func<int, IWidget, Point> updateLoc)
        {
            return wLists =>
            {
                var acc = wSize(wTree);
                for (var i = wLists.Count - 1; i >= 0; i--)
                {
                    var list = wLists[i];
                    for (var j = list.FirstWidgetIndex; j < list.LastWidgetIndex; j++)
                    {
                        var child = wTree.Children.ElementAt(j);
                        var newCoord = acc - wSize(child);
                        WidgetsSpaceHelper.UpdateSpace(child, new Rectangle(updateLoc(newCoord, child),
                            child.Space.Size));
                    }

                    acc -= listSize(list);
                }
            };
        }
    }
}