using System;
using System.Collections.Generic;

namespace WForest.UI.Properties.Grid.Row
{
    public class Row : IProperty
    {
        public int Priority { get; } = 1;

        internal List<WidgetsDataSubList> Rows = new List<WidgetsDataSubList>();
        public void ApplyOn(WidgetTree widgetNode)
        {
            
            if (widgetNode.Children.Count == 0)
                return;
            
            Rows = GridHelper.OrganizeWidgetsInRows(widgetNode);
        }
    }
}