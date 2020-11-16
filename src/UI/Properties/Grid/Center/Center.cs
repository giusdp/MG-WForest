using System.Linq;
using Serilog;

namespace PiBa.UI.Properties.Grid.Center
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

            var rowProps = widgetNode.Properties.OfType<Row.Row>().ToList();

            if (rowProps.Any())
            {
                CenterHandler.CenterByRow(widgetNode, rowProps.First().Rows);
            }
            else
            {
                var colProps = widgetNode.Properties.OfType<Column.Column>().ToList();
                if (colProps.Any())
                {
                    CenterHandler.CenterByColumn(widgetNode, colProps.First().Columns);
                }
            }
        }
    }
}