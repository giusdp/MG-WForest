using System;
using System.Linq;
using PiBa.UI.Properties.Grid.Column;
using PiBa.UI.Properties.Grid.Row;
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

            var rowProps = widgetNode.Properties.OfType<Row>().ToList();

            if (rowProps.Any())
            {
                CenterHandler.CenterByRow(widgetNode, rowProps.First().Rows);
            }
            else
            {
                var colProps = widgetNode.Properties.OfType<Column>().ToList();
                if (colProps.Any())
                {
                    CenterHandler.CenterByRow(widgetNode, colProps.First().Columns);
                }
                else
                {
                    // center both..
                }
            }

            // CenterHandler.CenterByRow(widgetNode);
        }
    }
}