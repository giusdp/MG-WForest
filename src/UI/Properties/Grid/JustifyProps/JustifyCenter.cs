using WForest.UI.Properties.Grid.Utils;
using WForest.UI.WidgetTrees;

namespace WForest.UI.Properties.Grid.JustifyProps
{
    public class JustifyCenter : Property
    {
        public override int Priority { get; } = 2;

        internal JustifyCenter(){}

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