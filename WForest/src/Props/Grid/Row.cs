using System;
using System.Collections.Generic;
using System.Linq;
using WForest.Exceptions;
using WForest.Props.Grid.Utils;
using WForest.Props.Interfaces;
using WForest.Utilities;
using WForest.Utilities.WidgetUtils;
using WForest.Widgets.Interfaces;

namespace WForest.Props.Grid
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

        /// <inheritdoc/>
        public bool IsApplied { get; set; }

        internal List<WidgetsDataSubList> Rows = new();

        /// <summary>
        /// Organizes widgets in a horizontal sequence. If the row exceeds the maximum width of the parent, it goes on
        /// a new row below.
        /// </summary>
        /// <param name="widget"></param>
        public void ApplyOn(IWidget widget)
        {
            IsApplied = false;
            if (widget.Props.Contains<Column>())
                throw new IncompatibleWidgetException("Cannot add Row prop if widget is already a Column");
            if (!widget.IsLeaf)
            {
                Rows = GridHelper.OrganizeWidgetsInRows(widget);
                UpdateSizeAfterBuildingRows(widget);
            }

            IsApplied = true;
            OnApplied();
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);

        private void UpdateSizeAfterBuildingRows(IWidget widget)
        {
            var nw = Rows.Max(r => r.Width);
            var nh = Rows.Sum(r => r.Height);
            var widthChanged = widget.Space.Width < nw;
            var heightChanged = widget.Space.Height < nh;

            if (!widthChanged && !heightChanged) return;

            nw = widthChanged ? nw : widget.Space.Width;
            nh = heightChanged ? nh : widget.Space.Height;
            WidgetSpaceHelper.UpdateSpace(widget, new RectangleF(widget.Space.X, widget.Space.Y, nw, nh));
        }
    }
}