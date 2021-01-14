using WForest.UI.Properties.Grid.Utils;
using WForest.UI.WidgetTrees;

namespace WForest.UI.Properties.Grid.ItemProps
{
    /// <summary>
    /// Property to move widgets to the center vertically in a Row or horizontally in a Column.
    /// Differently from JustifyCenter, this property deals with the opposite axis of a Row/Column.
    /// </summary>
    public class ItemCenter : Property
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
        /// <param name="widgetNode"></param>
        public override void ApplyOn(WidgetTree widgetNode)
        {
            ApplyUtils.ApplyIfThereAreChildren(widgetNode,
                $"{widgetNode.Data} has no children to item-center.",
                () =>
                {
                    if (ApplyUtils.TryExtractRows(widgetNode, out var rows))
                        CenterHelper.ItemCenterVertical(widgetNode, rows);
                    else if (ApplyUtils.TryExtractColumns(widgetNode, out var cols))
                        CenterHelper.ItemCenterHorizontal(widgetNode, cols);
                });
        }
    }
}