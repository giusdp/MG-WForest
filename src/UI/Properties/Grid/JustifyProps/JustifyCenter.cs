using Serilog;
using WForest.UI.Properties.Grid.Utils;

namespace WForest.UI.Properties.Grid.JustifyProps
{
    public class JustifyCenter : IProperty
    {
        public int Priority { get; } = 2;

        public void ApplyOn(WidgetTree widgetNode)
        {
            if (widgetNode.Children.Count == 0)
            {
                Log.Warning($"{widgetNode.Data} has no children to center.");
                return;
            }

            if (GridHelper.TryExtractRows(widgetNode, out var rows))
                CenterHelper.JustifyCenterByRow(widgetNode, rows);
            else if (GridHelper.TryExtractColumns(widgetNode, out var cols))
                CenterHelper.JustifyCenterByColumn(widgetNode, cols);
        }
    }
}