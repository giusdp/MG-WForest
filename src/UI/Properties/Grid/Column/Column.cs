using System.Collections.Generic;
using WForest.UI.Properties.Grid.Utils;
using WForest.UI.WidgetTrees;

namespace WForest.UI.Properties.Grid.Column
{
    public class Column : Property
    {
        public override int Priority { get; } = 1;

        internal List<WidgetsDataSubList> Columns = new List<WidgetsDataSubList>();

        internal Column(){}

        public override void ApplyOn(WidgetTree widgetNode)
        {
            if (widgetNode.Children.Count != 0)
                Columns = GridHelper.OrganizeWidgetsInColumns(widgetNode);
        }
    }
}