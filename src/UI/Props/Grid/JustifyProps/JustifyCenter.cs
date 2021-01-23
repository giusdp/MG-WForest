using WForest.UI.Props.Grid.Utils;
using WForest.UI.WidgetTrees;

namespace WForest.UI.Props.Grid.JustifyProps
{
    /// <summary>
    /// Property to move widgets to the center of a Row or Column.
    /// </summary>
    public class JustifyCenter : Prop
    {
        /// <summary>
        /// Since it changes the layout internally in a Row or Col, it should be applied after them.
        /// Row/Col have priority of 1 so this has priority of 2.
        /// </summary>
        public override int Priority { get; } = 2;

        internal JustifyCenter()
        {
        }

        /// <summary>
        /// Move the widgets to the center of a Row (centered horizontally) or of a Column (centered vertically).
        /// </summary>
        /// <param name="widgetNode"></param>
        public override void ApplyOn(WidgetTree widgetNode)
        {
            ApplyUtils.ApplyIfThereAreChildren(widgetNode,
                $"{widgetNode.Data} has no children to center.",
                () =>
                {
                    if (ApplyUtils.TryExtractRows(widgetNode, out var rows))
                        CenterHelper.JustifyCenterByRow(widgetNode, rows);
                    else if (ApplyUtils.TryExtractColumns(widgetNode, out var cols))
                        CenterHelper.JustifyCenterByColumn(widgetNode, cols);
                }
            );
        }
    }
}