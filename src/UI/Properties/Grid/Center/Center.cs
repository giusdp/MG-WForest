using System.Linq;
using Microsoft.Xna.Framework;
using Serilog;

namespace WForest.UI.Properties.Grid.Center
{
    public class Center : IProperty
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
            {
                CenterHelper.CenterByRow(widgetNode, rowProps.First().Rows);
                // update children position
                // widgetNode.Children.ForEach(c =>
                //     c.Data.Space = new Rectangle(widgetNode.Data.Space.Location, c.Data.Space.Size));
            }
            else
            {
                var colProps = widgetNode.Properties.OfType<Column.Column>().ToList();
                if (colProps.Any())
                {
                    CenterHelper.CenterByColumn(widgetNode, colProps.First().Columns);
                    // widgetNode.Children.ForEach(c =>
                    //     c.Data.Space = new Rectangle(widgetNode.Data.Space.Location, c.Data.Space.Size));
                }
            }
        }
    }
}