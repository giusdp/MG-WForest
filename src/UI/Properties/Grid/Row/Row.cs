using System.Collections.Generic;

namespace PiBa.UI.Properties.Grid.Row
{
    public class Row : IProperty
    {
        public int Priority { get; } = 0;

        internal List<WidgetsDataSubList> rows;
        public void ApplyOn(WidgetTree widgetNode)
        {
            if (widgetNode.Children.Count <= 1)
                return;

            
            rows = GridHandler.OrganizeWidgetsInRows(widgetNode);
        }
    }
}