using System;
using WForest.Exceptions;
using WForest.Props.Interfaces;
using WForest.Props.Props.Grid.Utils;
using WForest.Widgets.Interfaces;

namespace WForest.Props.Props.Grid.ItemProps
{
    /// <summary>
    /// Property to move widgets to the center vertically in a Row or horizontally in a Column.
    /// Differently from JustifyCenter, this property deals with the opposite axis of a Row/Column.
    /// </summary>
    public class ItemCenter : IApplicableProp
    {
        /// <summary>
        /// Since it changes the layout for the other axis in a Row or Col, it should be applied after the layout
        /// for the main axis is applied.
        /// </summary>
        public int Priority { get; set; } = 4;

        /// <inherit/>
        public event EventHandler? Applied;

        /// <inheritdoc/>
        public bool IsApplied { get; set; }

        /// <summary>
        /// Move the widgets to the vertical center of a Row or to the horizontal center of a Column.
        /// </summary>
        /// <param name="widget"></param>
        public void ApplyOn(IWidget widget)
        {
            IsApplied = false;
            ApplyUtils.ApplyIfThereAreChildren(widget,
                $"{widget} has no children to item-center.",
                () =>
                {
                    if (ApplyUtils.TryExtractRows(widget, out var rows))
                        CenterHelper.ItemCenterVertical(widget, rows);
                    else if (ApplyUtils.TryExtractColumns(widget, out var cols))
                        CenterHelper.ItemCenterHorizontal(widget, cols);
                    else
                    {
                        System.Diagnostics.Debug.WriteLine(
                            $"ItemCenter can only be applied to a Row or Column Widget! Make sure this {widget} has a Row or Column Prop",
                            "ERROR");
                        throw new IncompatibleWidgetException(
                            "Tried to apply ItemCenter to a widget without a Row or Column Prop");
                    }
                });
            IsApplied = false;
            OnApplied();
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);
    }
}