using System.Collections.Generic;

namespace WForest.UI.Properties.Grid.Column
{
    public class Column : IProperty
    {
        public int Priority { get; } = 1;

        internal List<WidgetsDataSubList> Columns = new List<WidgetsDataSubList>();

        public void ApplyOn(WidgetTree widgetNode)
        {
            if (widgetNode.Children.Count == 0)
                return;
            Columns = GridHelper.OrganizeWidgetsInColumns(widgetNode);
        }
    }
}