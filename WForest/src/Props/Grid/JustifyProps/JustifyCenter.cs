using System;
using Serilog;
using WForest.Exceptions;
using WForest.Props.Grid.Utils;
using WForest.Props.Interfaces;
using WForest.Widgets.Interfaces;

namespace WForest.Props.Grid.JustifyProps
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
        public bool ApplicationDone { get; set; }
        /// <summary>
        /// Move the widgets to the center of a Row (centered horizontally) or of a Column (centered vertically).
        /// </summary>
        /// <param name="widget"></param>
        public void ApplyOn(IWidget widget)
        {
            ApplicationDone = false;
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
                        Log.Error(
                            "JustifyCenter can only be applied to a Row or Column Widget! Make sure this {W} has a Row or Column Prop",
                            widget.ToString());
                        throw new IncompatibleWidgetException(
                            "Tried to apply JustifyCenter to a widget without a Row or Column Prop");
                    }
                }
            );
            ApplicationDone = true;
            OnApplied();
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);
    }
}