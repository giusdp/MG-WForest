using Serilog;

namespace WForest.UI.Properties.Grid.ItemProps
{
    public class ItemBase : IProperty
    {
        public int Priority { get; } = 3;
        public void ApplyOn(WidgetTree widgetNode)
        {
            if (widgetNode.Children.Count == 0)
            {
                Log.Warning($"{widgetNode.Data} has no children to item-center.");
                return;
            }
            throw new System.NotImplementedException();
        }
    }
}