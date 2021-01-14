using System.Collections.Generic;
using WForest.UI.Properties.Grid.Utils;
using WForest.UI.WidgetTrees;

namespace WForest.UI.Properties.Grid.Column
{
    /// <summary>
    /// Property that deals with the layout. It puts the children of the widget with this property in a vertical sequence.
    /// </summary>
    public class Column : Property
    {
        /// <summary>
        /// Column has a priority of 1, it should be applied after properties that directly update the space of a widget, such as Margin.
        /// </summary>
        public override int Priority { get; } = 1;

        internal List<WidgetsDataSubList> Columns = new List<WidgetsDataSubList>();

        internal Column(){}

        /// <summary>
        /// Organizes widgets in a vertical sequence. If the column exceeds the maximum height of the parent, it goes on
        /// a new column on the right.
        /// </summary>
        /// <param name="widgetNode"></param>
        public override void ApplyOn(WidgetTree widgetNode)
        {
            if (widgetNode.Children.Count != 0)
                Columns = GridHelper.OrganizeWidgetsInColumns(widgetNode);
        }
    }
}