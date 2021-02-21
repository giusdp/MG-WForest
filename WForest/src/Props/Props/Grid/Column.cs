using System;
using System.Collections.Generic;
using System.Linq;
using WForest.Exceptions;
using WForest.Props.Interfaces;
using WForest.Props.Props.Grid.Utils;
using WForest.Utilities;
using WForest.Utilities.WidgetUtils;
using WForest.Widgets.Interfaces;

namespace WForest.Props.Props.Grid
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
        public bool IsApplied { get; set; }

        internal List<WidgetsDataSubList> Columns = new();

        /// <summary>
        /// Organizes widgets in a vertical sequence. If the column exceeds the maximum height of the parent, it goes on
        /// a new column on the right.
        /// </summary>
        /// <param name="widget"></param>
        public void ApplyOn(IWidget widget)
        {
            IsApplied = false;
            if (widget.Props.Contains<Row>())
                throw new IncompatibleWidgetException("Cannot add Column prop if widget is already a Row");
            if (!widget.IsLeaf)
            {
                Columns = GridHelper.OrganizeWidgetsInColumns(widget);
                UpdateSizeAfterBuildingColumns(widget);
            }

            IsApplied = true;
            OnApplied();
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);

        private void UpdateSizeAfterBuildingColumns(IWidget widget)
        {
            var nw = Columns.Sum(r => r.Width);
            var nh = Columns.Max(r => r.Height);
            var widthChanged = widget.Space.Width < nw;
            var heightChanged = widget.Space.Height < nh;

            if (!widthChanged && !heightChanged) return;

            nw = widthChanged ? nw : widget.Space.Width;
            nh = heightChanged ? nh : widget.Space.Height;
            WidgetSpaceHelper.UpdateSpace(widget, new RectangleF(widget.Space.X, widget.Space.Y, nw, nh));
        }
    }
}