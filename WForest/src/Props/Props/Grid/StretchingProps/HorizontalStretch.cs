using System;
using System.Linq;
using WForest.Props.Interfaces;
using WForest.Props.Props.Grid.Utils;
using WForest.Utilities;
using WForest.Utilities.WidgetUtils;
using WForest.Widgets.Interfaces;

namespace WForest.Props.Props.Grid.StretchingProps
{
    /// <summary>
    /// Property to increase the width of the widget til the width of the parent.
    /// If the widget is the root, it does nothing since it has no parent.
    /// </summary>
    public class HorizontalStretch : IApplicableProp
    {
        /// <inheritdoc/>
        public int Priority { get; set; } = 2;

        /// <inherit/>
        public event EventHandler? Applied;

        /// <inheritdoc/>
        public bool IsApplied { get; set; }

        /// <summary>
        /// It gets the width of the parent (if it has one) and replaces the widget's width with it, then updates the spaces of its children.
        /// </summary>
        /// <param name="widget"></param>
        public void ApplyOn(IWidget widget)
        {
            IsApplied = false;
            if (widget.IsRoot) return;
            var (x, y, _, h) = widget.Space;

            if (!widget.Props.Contains<VerticalStretch>() && widget.Children.Any())
            {
                var childMaxHeight = widget.Children.Max(c => c.TotalSpaceOccupied.Height);
                if (childMaxHeight > h) h = childMaxHeight;
            }

            var (nw, nonFinishedHorSiblings) = ApplyUtils.StretchedWidthUsingWidthSiblings(widget);
            nonFinishedHorSiblings.ForEach(s =>
            {
                var hp = (IApplicableProp) s.Props.Get<HorizontalStretch>().First();
                hp.Applied += (_, _) =>
                {
                    IsApplied = false;
                    WidgetSpaceHelper.UpdateSpace(widget, new RectangleF(x, y, nw - s.Space.Width, h));
                    IsApplied = true;
                };
            });
            WidgetSpaceHelper.UpdateSpace(widget,
                new RectangleF(x, y, nw - widget.Margins.Left - widget.Margins.Right, h));
            IsApplied = true;
            OnApplied();
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);
    }
}