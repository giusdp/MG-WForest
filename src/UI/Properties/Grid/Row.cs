using System.Collections.Generic;
using WForest.UI.Properties.Grid.Utils;
using WForest.UI.WidgetTrees;

namespace WForest.UI.Properties.Grid
{
    /// <summary>
    /// Property that deals with the layout. It puts the children of the widget with this property in a horizontal sequence.
    /// </summary>
    public class Row : Property
    {
        /// <summary>
        /// Row has a priority of 1, it should be applied after properties that directly update the space of a widget, such as Margin.
        /// </summary>
        public override int Priority { get; } = 1;

        internal List<WidgetsDataSubList> Rows = new List<WidgetsDataSubList>();

        internal Row()
        {
        }

        /// <summary>
        /// Organizes widgets in a horizontal sequence. If the row exceeds the maximum width of the parent, it goes on
        /// a new row below.
        /// </summary>
        /// <param name="widgetNode"></param>
        public override void ApplyOn(WidgetTree widgetNode)
        {
            if (widgetNode.Children.Count != 0)
                Rows = GridHelper.OrganizeWidgetsInRows(widgetNode);
        }
    }
}