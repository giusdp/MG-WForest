using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Serilog;
using WForest.Exceptions;
using WForest.Props.Grid.Utils;
using WForest.Props.Interfaces;
using WForest.Utilities;
using WForest.Utilities.WidgetUtils;
using WForest.Widgets.Interfaces;

namespace WForest.Props.Grid.ItemProps
{
    /// <summary>
    /// Property to move widgets to the bottom in a Row or to the right in a Column.
    /// Differently from JustifyEnd, this property deals with the opposite axis of a Row/Column.
    /// </summary>
    public class ItemBase : IApplicableProp
    {
        /// <summary>
        /// Since it changes the layout for the other axis in a Row or Col, it should be applied after the layout
        /// for the main axis is applied.
        /// </summary>
        public int Priority { get; set; } = 4;

        /// <inherit/>
        public event EventHandler? Applied;

        /// <inheritdoc/>
        public bool ApplicationDone { get; set; }

        /// <summary>
        /// Move the widgets to the bottom of a Row or right of a Column.
        /// </summary>
        /// <param name="widget"></param>
        public void ApplyOn(IWidget widget)
        {
            ApplicationDone = false;
            ApplyUtils.ApplyIfThereAreChildren(widget, $"{widget} has no children to item-base.",
                () =>
                {
                    var rowsAtBase =
                        PutAtBase(widget, l => l.Height, GridHelper.WidgetHeight,
                            (y, c) => new Vector2(c.Space.X, y + c.Margins.Top));
                    var colsAtBase =
                        PutAtBase(widget, l => l.Width, GridHelper.WidgetWidth,
                            (x, c) => new Vector2(x + c.Margins.Left, c.Space.Y));

                    if (ApplyUtils.TryExtractRows(widget, out var rows))
                        rowsAtBase(rows);
                    else if (ApplyUtils.TryExtractColumns(widget, out var cols))
                        colsAtBase(cols);
                    else
                    {
                        Log.Error(
                            "ItemBase can only be applied to a Row or Column Widget! Make sure this {W} has a Row or Column Prop",
                            widget.ToString());
                        throw new IncompatibleWidgetException(
                            "Tried to apply ItemBase to a widget without a Row or Column Prop");
                    }
                });
            ApplicationDone = true;
            OnApplied();
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);

        private static Action<List<WidgetsDataSubList>> PutAtBase(IWidget wTree,
            Func<WidgetsDataSubList, float> listSize, Func<IWidget, float> wSize,
            Func<float, IWidget, Vector2> updateLoc)
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
                        WidgetsSpaceHelper.UpdateSpace(child, new RectangleF(updateLoc(newCoord, child),
                            child.Space.Size));
                    }

                    acc -= listSize(list);
                }
            };
        }
    }
}