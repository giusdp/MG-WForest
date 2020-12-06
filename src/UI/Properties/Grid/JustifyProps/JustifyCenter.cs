using WForest.UI.Properties.Grid.Utils;

namespace WForest.UI.Properties.Grid.JustifyProps
{
    public class JustifyCenter : Property
    {
        internal override int Priority { get; } = 2;

        internal override void ApplyOn(WidgetTree.WidgetTree widgetNode)
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