using System;
using System.Collections.Generic;
using WForest.UI.Properties.Grid.Utils;

namespace WForest.UI.Properties.Grid.Row
{
    public class Row : Property
    {
        internal override int Priority { get; } = 1;

        internal List<WidgetsDataSubList> Rows = new List<WidgetsDataSubList>();

        internal override void ApplyOn(WidgetTree widgetNode)
        {
            if (widgetNode.Children.Count != 0)
                Rows = GridHelper.OrganizeWidgetsInRows(widgetNode);
        }
    }
}