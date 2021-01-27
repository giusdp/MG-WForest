using System;
using WForest.UI.Props.Grid.Utils;
using WForest.UI.Props.Interfaces;
using WForest.UI.Widgets.Interfaces;

namespace WForest.UI.Props.Grid.JustifyProps
{
    /// <summary>
    /// Property to move widgets to the center of a Row or Column.
    /// </summary>
    public class JustifyCenter : IApplicableProp
    {
        /// <summary>
        /// Since it changes the layout internally in a Row or Col, it should be applied after them.
        /// Row/Col have priority of 1 so this has priority of 2.
        /// </summary>
        public int Priority { get; set; } = 2;

        public event EventHandler? Applied;

        internal JustifyCenter()
        {
        }

        /// <summary>
        /// Move the widgets to the center of a Row (centered horizontally) or of a Column (centered vertically).
        /// </summary>
        /// <param name="widget"></param>
        public void ApplyOn(IWidget widget)
        {
            ApplyUtils.ApplyIfThereAreChildren(widget,
                $"{widget} has no children to center.",
                () =>
                {
                    if (ApplyUtils.TryExtractRows(widget, out var rows))
                        CenterHelper.JustifyCenterByRow(widget, rows);
                    else if (ApplyUtils.TryExtractColumns(widget, out var cols))
                        CenterHelper.JustifyCenterByColumn(widget, cols);
                }
            );
            OnApplied();
        }

        protected virtual void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);
    }
}