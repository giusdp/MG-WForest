using WForest.UI.Props.Grid.Utils;
using WForest.UI.Widgets;

namespace WForest.UI.Props.Grid.ItemProps
{
    /// <summary>
    /// Property to move widgets to the center vertically in a Row or horizontally in a Column.
    /// Differently from JustifyCenter, this property deals with the opposite axis of a Row/Column.
    /// </summary>
    public class ItemCenter : Prop
    {
        /// <summary>
        /// Since it changes the layout for the other axis in a Row or Col, it should be applied after the layout
        /// for the main axis is applied.
        /// Row/Col have priority of 1, the Justify properties have priority of 2, so this has priority of 3.
        /// </summary>
        public override int Priority { get; } = 3;

        internal ItemCenter()
        {
        }

        /// <summary>
        /// Move the widgets to the vertical center of a Row or to the horizontal center of a Column.
        /// </summary>
        /// <param name="widget"></param>
        public override void ApplyOn(IWidget widget)
        {
            ApplyUtils.ApplyIfThereAreChildren(widget,
                $"{widget} has no children to item-center.",
                () =>
                {
                    if (ApplyUtils.TryExtractRows(widget, out var rows))
                        CenterHelper.ItemCenterVertical(widget, rows);
                    else if (ApplyUtils.TryExtractColumns(widget, out var cols))
                        CenterHelper.ItemCenterHorizontal(widget, cols);
                });
        }
    }
}