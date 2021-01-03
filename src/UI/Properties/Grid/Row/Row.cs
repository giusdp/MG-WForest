using System.Collections.Generic;
using WForest.UI.Properties.Grid.Utils;
using WForest.UI.WidgetTrees;

namespace WForest.UI.Properties.Grid.Row
{
    internal class Row : Property
    {
        internal override int Priority { get; } = 1;

        internal List<WidgetsDataSubList> Rows = new List<WidgetsDataSubList>();

        internal Row(){}
        internal override void ApplyOn(WidgetTree widgetNode)
        {
            if (widgetNode.Children.Count != 0)
                Rows = GridHelper.OrganizeWidgetsInRows(widgetNode);
        }
    }
}