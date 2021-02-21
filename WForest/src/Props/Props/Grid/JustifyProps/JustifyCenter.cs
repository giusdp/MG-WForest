using System;
using WForest.Exceptions;
using WForest.Props.Interfaces;
using WForest.Props.Props.Grid.Utils;
using WForest.Widgets.Interfaces;

namespace WForest.Props.Props.Grid.JustifyProps
{
    /// <summary>
    /// Property to move widgets to the center of a Row or Column.
    /// </summary>
    public class JustifyCenter : IApplicableProp
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
        /// Move the widgets to the center of a Row (centered horizontally) or of a Column (centered vertically).
        /// </summary>
        /// <param name="widget"></param>
        public void ApplyOn(IWidget widget)
        {
            IsApplied = false;
            ApplyUtils.ApplyIfThereAreChildren(widget,
                $"{widget} has no children to center.",
                () =>
                {
                    if (ApplyUtils.TryExtractRows(widget, out var rows))
                        CenterHelper.JustifyCenterByRow(widget, rows);
                    else if (ApplyUtils.TryExtractColumns(widget, out var cols))
                        CenterHelper.JustifyCenterByColumn(widget, cols);
                    else
                    {
                        System.Diagnostics.Debug.WriteLine(
                            $"JustifyCenter can only be applied to a Row or Column Widget! Make sure this {widget} has a Row or Column Prop",
                            "ERROR");
                        throw new IncompatibleWidgetException(
                            "Tried to apply JustifyCenter to a widget without a Row or Column Prop");
                    }
                }
            );
            IsApplied = true;
            OnApplied();
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);
    }
}