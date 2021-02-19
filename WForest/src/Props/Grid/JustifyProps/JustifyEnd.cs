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

namespace WForest.Props.Grid.JustifyProps
{
    /// <summary>
    /// Property to move widgets to the end of a Row or Column.
    /// </summary>
    public class JustifyEnd : IApplicableProp
    {
        /// <summary>
        /// Since it changes the layout internally in a Row or Col, it should be applied after them.
        /// </summary>
        public int Priority { get; set; } = 3;

        /// <inherit/>
        public event EventHandler? Applied;

        /// <inheritdoc/>
        public bool IsApplied { get; set; }

        /// <summary>
        /// Move the widgets to the end of a Row (to the right) or of a Col (to the bottom).
        /// </summary>
        /// <param name="widget"></param>
        public void ApplyOn(IWidget widget)
        {
            IsApplied = false;
            ApplyUtils.ApplyIfThereAreChildren(widget,
                $"{widget} has no children to justify-end.",
                () =>
                {
                    var rowsAtEnd =
                        PutAtEnd(widget, GridHelper.WidgetWidth, (x, c) => new Vector2(x, c.Space.Y),
                            c => c.Margins.Left);
                    var colsAtEnd =
                        PutAtEnd(widget, GridHelper.WidgetHeight, (y, c) => new Vector2(c.Space.X, y),
                            c => c.Margins.Top);

                    if (ApplyUtils.TryExtractRows(widget, out var rows))
                        rowsAtEnd(rows);
                    else if (ApplyUtils.TryExtractColumns(widget, out var cols))
                        colsAtEnd(cols);
                    else
                    {
                        Log.Error(
                            "JustifyEnd can only be applied to a Row or Column Widget! Make sure this {W} has a Row or Column Prop",
                            widget.ToString());
                        throw new IncompatibleWidgetException(
                            "Tried to apply JustifyEnd to a widget without a Row or Column Prop");
                    }
                });
            IsApplied = true;
            OnApplied();
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);

        private static Action<List<WidgetsDataSubList>> PutAtEnd(IWidget wTree, Func<IWidget, float> getSize,
            Func<float, IWidget, Vector2> updateLoc, Func<IWidget, float> getMargin)
        {
            return wLists =>
                wLists.ForEach(r =>
                {
                    var acc = getSize(wTree);
                    for (var i = r.LastWidgetIndex - 1; i >= r.FirstWidgetIndex; i--)
                    {
                        var child = wTree.Children.ElementAt(i);
                        acc -= getSize(child) - getMargin(child);
                        WidgetSpaceHelper.UpdateSpace(child, new RectangleF(updateLoc(acc, child),
                            child.Space.Size));
                        acc -= getMargin(child);
                    }
                });
        }
    }
}