using WForest.UI.Properties.Grid.Utils;

namespace WForest.UI.Properties.Grid.JustifyProps
{
    public class JustifyCenter : IProperty
    {
        public int Priority { get; } = 2;

        public void ApplyOn(WidgetTree widgetNode)
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