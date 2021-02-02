using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using WForest.Exceptions;
using WForest.UI.Props.Grid.Utils;
using WForest.UI.Props.Interfaces;
using WForest.UI.Widgets.Interfaces;

namespace WForest.UI.Props.Grid
{
    /// <summary>
    /// Property that deals with the layout. It puts the children of the widget with this property in a vertical sequence.
    /// </summary>
    public class Column : IApplicableProp
    {
        /// <summary>
        /// Column has a priority of 1, it should be applied after properties that directly update the space of a widget, such as Margin.
        /// </summary>
        public int Priority { get; set; } = 1;

        /// <inheritdoc/>
        public event EventHandler? Applied;

        /// <inheritdoc/>
        public bool ApplicationDone { get; set; }

        internal List<WidgetsDataSubList> Columns = new List<WidgetsDataSubList>();

        /// <summary>
        /// Organizes widgets in a vertical sequence. If the column exceeds the maximum height of the parent, it goes on
        /// a new column on the right.
        /// </summary>
        /// <param name="widget"></param>
        public void ApplyOn(IWidget widget)
        {
            ApplicationDone = false;
            if (widget.Props.Contains<Row>())
                throw new IncompatibleWidgetException("Cannot add Column prop if widget is already a Row.");
            if (!widget.IsLeaf)
                Columns = GridHelper.OrganizeWidgetsInColumns(widget);
            ApplicationDone = true;
            OnApplied();
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);
    }
}