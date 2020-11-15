using Serilog;

namespace PiBa.UI.Properties.Row
{
    public class Row : IProperty
    {
        public int Priority { get; } = 0;

        public void ApplyOn(WidgetTree widgetNode)
        {
            if (widgetNode.Children.Count == 0)
            {
                Log.Warning(
                    $"{widgetNode.Data} has no children to center.");
                return;
            }
            
            RowHandler.SetWidgetsInRows(widgetNode);
        }
    }
}