using System;
using WForest.Props.Interfaces;
using WForest.Utilities;
using WForest.Utilities.WidgetUtils;
using WForest.Widgets.Interfaces;

namespace WForest.Props
{
    /// <summary>
    /// Margin property that adds margin space on a widget on all four directions.
    /// </summary>
    public class Margin : IApplicableProp
    {
        /// <summary>
        /// Margin should be one of the first to be applied.
        /// </summary>
        public int Priority { get; set; } = 0;

        /// <inherit/>
        public event EventHandler? Applied;

        /// <inheritdoc/>
        public bool ApplicationDone { get; set; }

        private readonly MarginValues _marginValues;

        public Margin(int marginLeft, int marginRight, int marginTop, int marginBottom)
        {
            _marginValues = new MarginValues(marginLeft, marginRight, marginTop, marginBottom);
        }

        /// <summary>
        /// Adds margin space to the widget. It updates the space required by the widget and applies the changes to all the children.
        /// </summary>
        /// <param name="widget"></param>
        public void ApplyOn(IWidget widget)
        {
            ApplicationDone = false;
            
            var (x, y, w, h) = widget.Space;
            widget.Margins = AddMargin(widget);

            var newSpace = new RectangleF(x + _marginValues.Left, y + _marginValues.Top, w, h);
            WidgetsSpaceHelper.UpdateSpace(widget, newSpace);

            ApplicationDone = true;
            OnApplied();
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);

        private MarginValues AddMargin(IWidget widget)
        {
            var m = widget.Margins;
            return new MarginValues(m.Left + _marginValues.Left, m.Right + _marginValues.Right,
                m.Top + _marginValues.Top,
                m.Bottom + _marginValues.Bottom);
        }
    }
}