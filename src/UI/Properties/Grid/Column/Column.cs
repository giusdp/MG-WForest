namespace PiBa.UI.Properties.Grid.Column
{
    public class Column : IProperty
    {
        public int Priority { get; } = 0;

        public void ApplyOn(WidgetTree widgetNode)
        {
            if (widgetNode.Children.Count == 0)
                return;
            GridHandler.OrganizeWidgetsInColumns(widgetNode);
        }
    }
}