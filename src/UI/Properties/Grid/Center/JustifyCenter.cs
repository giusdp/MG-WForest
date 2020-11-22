using System.Linq;
using Serilog;

namespace WForest.UI.Properties.Grid.Center
{
    public class JustifyCenter : IProperty
    {
        public int Priority { get; } = 2;

        public void ApplyOn(WidgetTree widgetNode)
        {
            if (widgetNode.Children.Count == 0)
            {
                Log.Warning($"{widgetNode.Data} has no children to center.");
                return;
            }

            var rowProps = widgetNode.Properties.OfType<Row.Row>().ToList();

            if (rowProps.Any())
                CenterHelper.CenterByRow(widgetNode, rowProps.First().Rows);
            else
            {
                var colProps = widgetNode.Properties.OfType<Column.Column>().ToList();
                if (colProps.Any())
                    CenterHelper.CenterByColumn(widgetNode, colProps.First().Columns);
            }
        }
    }
}