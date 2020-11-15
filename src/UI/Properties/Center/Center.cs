using Serilog;

namespace PiBa.UI.Properties.Center
{
    public class Center : IProperty
    {
        public int Priority { get; } = 1;
        public void ApplyOn(WidgetTree widgetNode)
        {
            if (widgetNode.Children.Count == 0)
            {
                Log.Warning(
                    $"{widgetNode.Data} has no children to center.");
                return;
            }

            CenterHandler.CenterByRow(widgetNode);
        }
    }
}