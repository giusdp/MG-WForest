using WForest.UI.Properties.Grid.Utils;

namespace WForest.UI.Properties.Grid.ItemProps
{
    public class ItemBase : IProperty
    {
        public int Priority { get; } = 3;
        public void ApplyOn(WidgetTree widgetNode)
        {
            ApplyUtils.ApplyIfThereAreChildren(widgetNode, $"{widgetNode.Data} has no children to item-base.",
                () =>
                {
                    
                });
        }
    }
}