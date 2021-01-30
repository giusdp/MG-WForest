using System;
using System.Linq;
using WForest.UI.Props.Interfaces;
using WForest.UI.Utils;
using WForest.UI.Widgets.Interfaces;
using WForest.Utilities;

namespace WForest.UI.Props.Grid.StretchingProps
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

        /// <summary>
        /// It gets the width of the parent (if it has one) and replaces the widget's width with it, then updates the spaces of its children.
        /// </summary>
        /// <param name="widget"></param>
        public void ApplyOn(IWidget widget)
        {
            if (widget.IsRoot) return;
            var (x, y, _, h) = widget.Space;
            var nw = CalculateStretchedWidth(widget);

            if (!widget.Props.Contains<VerticalStretch>())
                h = widget.Children.Any() ? widget.Children.Max(c => c.Space.Height) : h;

            WidgetsSpaceHelper.UpdateSpace(widget,
                new RectangleF(x, y, nw, h));
            OnApplied();
        }

        private static float CalculateStretchedWidth(IWidget widget)
        {
            var nw = widget.Parent!.Space.Width;

            if (!widget.Parent.Props.SafeGetByProp<Row>().TryGetValue(out var rows)) return nw;
            if (rows!.FirstOrDefault() == null) return nw;

            var siblings = widget.Parent.Children.Where(w => w != widget);
            var count = 1;
            float nonStretchedWidth = 0;
            foreach (var sibling in siblings)
            {
                if (sibling.Props.Contains<HorizontalStretch>()) count++;
                else nonStretchedWidth += sibling.Space.Width;
            }

            nw -= nonStretchedWidth;
            return nw / count;
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);
    }
}