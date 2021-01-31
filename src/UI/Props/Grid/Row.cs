using System;
using System.Collections.Generic;
using WForest.Exceptions;
using WForest.UI.Props.Grid.Utils;
using WForest.UI.Props.Interfaces;
using WForest.UI.Widgets.Interfaces;

namespace WForest.UI.Props.Grid
{
    /// <summary>
    /// Property that deals with the layout. It puts the children of the widget with this property in a horizontal sequence.
    /// </summary>
    public class Row : IApplicableProp
    {
        /// <summary>
        /// Row has a priority of 1, it should be applied after properties that directly update the space of a widget, such as Margin.
        /// </summary>
        public int Priority { get; set; } = 1;

        /// <inheritdoc/>
        public event EventHandler? Applied;

        internal List<WidgetsDataSubList> Rows = new List<WidgetsDataSubList>();

        /// <summary>
        /// Organizes widgets in a horizontal sequence. If the row exceeds the maximum width of the parent, it goes on
        /// a new row below.
        /// </summary>
        /// <param name="widget"></param>
        public void ApplyOn(IWidget widget)
        {
            if (widget.Props.Contains<Column>())
                throw new IncompatibleWidgetException("Cannot add Row prop if widget is already a Column.");
            if (!widget.IsLeaf)
                Rows = GridHelper.OrganizeWidgetsInRows(widget);
            OnApplied();
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);
    }
}