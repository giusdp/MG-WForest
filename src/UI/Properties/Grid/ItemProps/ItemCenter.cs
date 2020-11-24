using Serilog;
using WForest.UI.Properties.Grid.Utils;

namespace WForest.UI.Properties.Grid.ItemProps
{
    public class ItemCenter : IProperty
    {
        public int Priority { get; } = 3;

        public void ApplyOn(WidgetTree widgetNode)
        {
            if (widgetNode.Children.Count == 0)
            {
                Log.Warning($"{widgetNode.Data} has no children to item-center.");
                return;
            }
            if (GridHelper.TryExtractRows(widgetNode, out var rows))
                CenterHelper.ItemCenterVertical(widgetNode, rows);
            else if (GridHelper.TryExtractColumns(widgetNode, out var cols))
                CenterHelper.ItemCenterHorizontal(widgetNode, cols);
        }
    }
}