using System.Collections.Generic;

namespace PiBa.UI.Properties.Grid.Column
{
    public class Column : IProperty
    {
        public int Priority { get; } = 0;

        internal List<WidgetsDataSubList> Columns = new List<WidgetsDataSubList>();

        public void ApplyOn(WidgetTree widgetNode)
        {
            if (widgetNode.Children.Count == 0)
                return;
            Columns = GridHandler.OrganizeWidgetsInColumns(widgetNode);
        }
    }
}