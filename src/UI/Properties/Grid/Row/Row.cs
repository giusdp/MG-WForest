using System.Collections.Generic;

namespace PiBa.UI.Properties.Grid.Row
{
    public class Row : IProperty
    {
        public int Priority { get; } = 0;

        internal List<WidgetsDataSubList> Rows = new List<WidgetsDataSubList>();
        public void ApplyOn(WidgetTree widgetNode)
        {
            if (widgetNode.Children.Count == 0)
                return;

            
            Rows = GridHelper.OrganizeWidgetsInRows(widgetNode);
        }
    }
}