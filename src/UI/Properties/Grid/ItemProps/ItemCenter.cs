using WForest.UI.Properties.Grid.Utils;

namespace WForest.UI.Properties.Grid.ItemProps
{
    public class ItemCenter : IProperty
    {
        public int Priority { get; } = 3;

        public void ApplyOn(WidgetTree widgetNode)
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