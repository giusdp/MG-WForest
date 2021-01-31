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
using WForest.Utilities;
using static WForest.UI.Props.Grid.Utils.GridHelper;

namespace WForest.UI.Props.Grid.JustifyProps
{
    /// <summary>
    /// Property to separate widgets in a Row or Column, setting maximum space in between them and from the window borders.
    /// </summary>
    public class JustifyAround : IApplicableProp
    {
        /// <summary>
        /// Since it changes the layout internally in a Row or Col, it should be applied after them.
        /// </summary>
        public int Priority { get; set; } = 3;

        /// <inherit/>
        public event EventHandler? Applied;

        /// <summary>
        /// Move the widgets in a way to have them separated between them and the window border.
        /// In a Row they are separated horizontally, in a Column vertically.
        /// </summary>
        /// <param name="widget"></param>
        public void ApplyOn(IWidget widget)
        {
            ApplyUtils.ApplyIfThereAreChildren(widget,
                $"{widget} has no children to justify space between.",
                () =>
                {
                    if (ApplyUtils.TryExtractRows(widget, out var rows))
                        SpaceAroundHorizontally(widget, rows);
                    else if (ApplyUtils.TryExtractColumns(widget, out var cols))
                        SpaceAroundVertically(widget, cols);
                    else
                    {
                        Log.Error(
                            "JustifyAround can only be applied to a Row or Column Widget! Make sure this {W} has a Row or Column Prop",
                            widget);
                        throw new IncompatibleWidgetException(
                            "Tried to apply JustifyAround to a widget without a Row or Column Prop");
                    }
                });
            OnApplied();
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);

        private static void SpaceAroundHorizontally(IWidget wTree, List<WidgetsDataSubList> lists)
        {
            float start = wTree.Space.X;
            float size = WidgetWidth(wTree);
            lists.ForEach(r =>
                DivideSpaceEvenly(start, size,
                    wTree.Children.ToList().GetRange(r.FirstWidgetIndex, r.LastWidgetIndex - r.FirstWidgetIndex),
                    WidgetWidth,
                    (c, p) => new Vector2(p + c.Margins.Left, c.Space.Y))
            );
        }

        private static void SpaceAroundVertically(IWidget wTree, List<WidgetsDataSubList> lists)
        {
            float start = wTree.Space.Y;
            float size = WidgetHeight(wTree);
            lists.ForEach(r =>
                DivideSpaceEvenly(start, size,
                    wTree.Children.ToList().GetRange(r.FirstWidgetIndex, r.LastWidgetIndex - r.FirstWidgetIndex),
                    WidgetHeight,
                    (c, p) => new Vector2(c.Space.X, p + c.Margins.Top))
            );
        }

        private static void DivideSpaceEvenly(float start, float parentSize, ICollection<IWidget> widgets,
            Func<IWidget, float> getSize, Func<IWidget, float, Vector2> updateLoc)
        {
            float usedPixels = widgets.Sum(getSize);
            float freePixels = parentSize - usedPixels;
            float spaceBetween = freePixels / (widgets.Count + 1.0f);
            float startPoint = start + spaceBetween;
            start += (int) Math.Round(spaceBetween);

            foreach (var w in widgets)
            {
                WidgetsSpaceHelper.UpdateSpace(w, new RectangleF(updateLoc(w, start), w.Space.Size));
                startPoint += getSize(w) + spaceBetween;
                start = (int) Math.Round(startPoint);
            }
        }
    }
}