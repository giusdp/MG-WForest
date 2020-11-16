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
                var rows = rowProps.First().Rows;

                var x = CenterHandler.RowHorizontalCenterCoord(widgetNode.Data.Space, rows.Max(r => r.Width));
                var y = CenterHandler.CenteredRowsVerticalCoord(widgetNode.Data.Space, rows.Sum(r => r.Height));
                rows.ForEach(r =>
                {
                    r.X = x;
                    r.Y = y;
                });

                CenterHandler.OffsetRowsHeights(rows);
                CenterHandler.CenterChildren(rows, widgetNode.Children);
            }
            else if (widgetNode.Properties.OfType<Column>().Any())
            {
                //center vertically
            }
            else
            {
                // center both
            }

            // CenterHandler.CenterByRow(widgetNode);
        }
    }
}