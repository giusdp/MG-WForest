using System.Collections.Generic;
using System.Linq;
using Serilog;

namespace WForest.UI.Properties.Grid.Center
{
    public class ItemCenter : IProperty
    {
        public int Priority { get; } = 3;

        public void ApplyOn(WidgetTree widgetNode)
        {
            if (widgetNode.Children.Count == 0)
            {
                Log.Warning(
                    $"{widgetNode.Data} has no children to item-center.");
                return;
            }

            if (TryExtractRows(widgetNode, out var rows))
            {
               CenterHelper.ItemCenterVertical(widgetNode, rows); 
            }
            else
            {
                var colProps = widgetNode.Properties.OfType<Column.Column>().ToList();
                if (colProps.Any())
                {
                    CenterHelper.ItemCenterHorizontal(widgetNode, colProps.First().Columns);
                }
            }
        }

        private static bool TryExtractRows(WidgetTree widgetNode, out List<WidgetsDataSubList> rows)
        {
            var rowProps = widgetNode.Properties.OfType<Row.Row>().ToList();
            if (rowProps.Any())
            {
                rows = rowProps.First().Rows;
                return true;
            }

            rows = new List<WidgetsDataSubList>();
            return false;
        }
    }
}