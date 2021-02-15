using System;
using System.Linq;
using Serilog;
using WForest.Exceptions;
using WForest.Props.Interfaces;
using WForest.UI.Props.Grid.Utils;
using WForest.Utilities;
using WForest.Utilities.WidgetUtils;
using WForest.Widgets.Interfaces;

namespace WForest.Props.Grid.StretchingProps
{
    /// <summary>
    /// Property to make the space of the widget flexible. It stays as little as possible and grows to take the minimum
    /// space required to accomodate the children.
    /// </summary>
    public class Flex : IApplicableProp
    {
        /// <inheritdoc/>
        public int Priority { get; set; }

        /// <inherit/>
        public event EventHandler? Applied;

        /// <inheritdoc/>
        public bool ApplicationDone { get; set; }

        /// <summary>
        /// Gets the children of the widget and expands the space enough to accomodate them,
        /// depending on if the parent is Row or Column.
        /// </summary>
        /// <param name="widget"></param>
        public void ApplyOn(IWidget widget)
        {
            ApplicationDone = false;
            IncreaseSpaceWithChildren(widget);

            var (x, y, _, _) = widget.Space;

            var maybeRow = ApplyUtils.ExtractRowProp(widget);
            if (maybeRow.TryGetValue(out var row))
            {
                row.Applied += (sender, args) =>
                {
                    WidgetsSpaceHelper.UpdateSpace(widget,
                        new RectangleF(
                            x,
                            y,
                            row.Rows.Max(r => r.Width),
                            row.Rows.Sum(r => r.Height)));
                };
            }
            else
            {
                var maybeCol = ApplyUtils.ExtractColumnProp(widget);
                if (maybeCol.TryGetValue(out var col))
                {
                    col.Applied += (sender, args) =>
                    {
                        WidgetsSpaceHelper.UpdateSpace(widget,
                            new RectangleF(
                                x,
                                y,
                                col.Columns.Sum(c => c.Width),
                                col.Columns.Sum(c => c.Height)));
                    };
                }
                else
                {
                    Log.Error(
                        "Flex can only be applied to a Row or Column Widget! Make sure the widget you applied Flex to has a Row or Column Prop");
                    throw new IncompatibleWidgetException(
                        "Tried to apply Flex to a widget without a Row or Column Prop");
                }
            }

            ApplicationDone = true;
            OnApplied();
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);

        private static void IncreaseSpaceWithChildren(IWidget widgetNode)
        {
            float w = 0, h = 0;
            foreach (var c in widgetNode.Children)
            {
                var (x, y, _, _) = widgetNode.Space;
                var (_, _, cw, ch) = c.Space;
                w += cw;
                h += ch;
                if (widgetNode.IsRoot == false)
                {
                    var (_, _, parentW, parentH) = widgetNode.Parent!.Space;
                    if (w > parentW) w -= cw;
                    if (h > parentH) h -= ch;
                }

                WidgetsSpaceHelper.UpdateSpace(widgetNode, new RectangleF(x, y, w, h));
            }
        }
    }
}