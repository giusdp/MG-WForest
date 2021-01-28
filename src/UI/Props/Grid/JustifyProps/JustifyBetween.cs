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
using static WForest.UI.Props.Grid.Utils.GridHelper;

namespace WForest.UI.Props.Grid.JustifyProps
{
    /// <summary>
    /// Property to separate widgets in a Row or Column, setting maximum space in between them.
    /// </summary>
    public class JustifyBetween : IApplicableProp
    {
        /// <summary>
        /// Since it changes the layout internally in a Row or Col, it should be applied after them.
        /// Row/Col have priority of 1 so this has priority of 2.
        /// </summary>
        public int Priority { get; set; } = 2;

        /// <inherit/>
        public event EventHandler? Applied;

        internal JustifyBetween()
        {
        }

        /// <summary>
        /// Move the widgets in a way to have them maximally separated between them.
        /// In a Row they are separated horizontally, in a Column vertically.
        /// </summary>
        /// <param name="widget"></param>
        public void ApplyOn(IWidget widget)
        {
            ApplyUtils.ApplyIfThereAreChildren(widget,
                $"{widget} has no children to justify space between.",
                () =>
                {
                    if (widget.Children.Count == 1) return;
                    if (ApplyUtils.TryExtractRows(widget, out var rows))
                        SpaceBetweenHorizontally(widget, rows);
                    else if (ApplyUtils.TryExtractColumns(widget, out var cols))
                        SpaceBetweenVertically(widget, cols);
                    else
                    {
                        Log.Error(
                            "JustifyBetween can only be applied to a Row or Column Widget! Make sure this {W} has a Row or Column Prop",
                            widget);
                        throw new IncompatibleWidgetException(
                            "Tried to apply JustifyBetween to a widget without a Row or Column Prop");
                    }
                });
            OnApplied();
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);


        private static void SpaceBetweenHorizontally(IWidget wTree, List<WidgetsDataSubList> lists)
        {
            int start = wTree.Space.X;
            int size = WidgetWidth(wTree);
            lists.ForEach(r =>
                DivideSpaceEvenly(start, size,
                    wTree.Children.ToList().GetRange(r.FirstWidgetIndex, r.LastWidgetIndex - r.FirstWidgetIndex),
                    WidgetWidth,
                    (c, p) => new Point(p + c.Margins.Left, c.Space.Y))
            );
        }

        private static void SpaceBetweenVertically(IWidget wTree, List<WidgetsDataSubList> lists)
        {
            int start = wTree.Space.Y;
            int size = WidgetHeight(wTree);
            lists.ForEach(r =>
                DivideSpaceEvenly(start, size,
                    wTree.Children.ToList().GetRange(r.FirstWidgetIndex, r.LastWidgetIndex - r.FirstWidgetIndex),
                    WidgetHeight,
                    (c, pos) => new Point(c.Space.X, pos + c.Margins.Top))
            );
        }

        private static void DivideSpaceEvenly(int start, int parentSize, List<IWidget> widgets,
            Func<IWidget, int> getSize, Func<IWidget, int, Point> updateLoc)
        {
            float startPoint = start;
            float usedPixels = widgets.Sum(getSize);
            float freePixels = parentSize - usedPixels;
            float spaceBetween = freePixels / (widgets.Count - 1.0f);

            widgets.ForEach(w =>
            {
                WidgetsSpaceHelper.UpdateSpace(w, new Rectangle(updateLoc(w, start), w.Space.Size));
                startPoint += getSize(w) + spaceBetween;
                start = (int) Math.Round(startPoint);
            });
        }
    }
}