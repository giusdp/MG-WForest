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

namespace WForest.UI.Props.Grid.JustifyProps
{
    /// <summary>
    /// Property to move widgets to the end of a Row or Column.
    /// </summary>
    public class JustifyEnd : IApplicableProp
    {
        /// <summary>
        /// Since it changes the layout internally in a Row or Col, it should be applied after them.
        /// Row/Col have priority of 1 so this has priority of 2.
        /// </summary>
        public int Priority { get; set; } = 2;

        public event EventHandler? Applied;

        internal JustifyEnd()
        {
        }

        /// <summary>
        /// Move the widgets to the end of a Row (to the right) or of a Col (to the bottom).
        /// </summary>
        /// <param name="widget"></param>
        public void ApplyOn(IWidget widget)
        {
            ApplyUtils.ApplyIfThereAreChildren(widget,
                $"{widget} has no children to justify-end.",
                () =>
                {
                    var rowsAtEnd =
                        PutAtEnd(widget, GridHelper.WidgetWidth, (x, c) => new Point(x, c.Space.Y),
                            c => c.Margins.Left);
                    var colsAtEnd =
                        PutAtEnd(widget, GridHelper.WidgetHeight, (y, c) => new Point(c.Space.X, y),
                            c => c.Margins.Top);

                    if (ApplyUtils.TryExtractRows(widget, out var rows))
                        rowsAtEnd(rows);
                    else if (ApplyUtils.TryExtractColumns(widget, out var cols))
                        colsAtEnd(cols);
                    else
                    {
                        Log.Error(
                            "JustifyEnd can only be applied to a Row or Column Widget! Make sure this {W} has a Row or Column Prop",
                            widget);
                        throw new IncompatibleWidgetException(
                            "Tried to apply JustifyEnd to a widget without a Row or Column Prop");
                    }
                });
            OnApplied();
        }

        protected virtual void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);

        private static Action<List<WidgetsDataSubList>> PutAtEnd(IWidget wTree, Func<IWidget, int> getSize,
            Func<int, IWidget, Point> updateLoc, Func<IWidget, int> getMargin)
        {
            return wLists =>
                wLists.ForEach(r =>
                {
                    var acc = getSize(wTree);
                    for (var i = r.LastWidgetIndex - 1; i >= r.FirstWidgetIndex; i--)
                    {
                        var child = wTree.Children.ElementAt(i);
                        acc -= getSize(child) - getMargin(child);
                        WidgetsSpaceHelper.UpdateSpace(child, new Rectangle(updateLoc(acc, child),
                            child.Space.Size));
                        acc -= getMargin(child);
                    }
                });
        }
    }
}