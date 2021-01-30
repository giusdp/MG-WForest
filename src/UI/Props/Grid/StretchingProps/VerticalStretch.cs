using System;
using System.Linq;
using WForest.UI.Props.Interfaces;
using WForest.UI.Utils;
using WForest.UI.Widgets.Interfaces;
using WForest.Utilities;

namespace WForest.UI.Props.Grid.StretchingProps
{
    /// <summary>
    /// Property to increase the height of the widget til the height of the parent.
    /// If the widget is the root, it does nothing since it has no parent.
    /// </summary>
    public class VerticalStretch : IApplicableProp
    {
        /// <inheritdoc/>
        public int Priority { get; set; } = 2;

        /// <inherit/>
        public event EventHandler? Applied;

        /// <summary>
        /// It gets the height of the parent (if it has one) and replaces the widget's height with it, then updates the spaces of its children.
        /// </summary>
        /// <param name="widget"></param>
        public void ApplyOn(IWidget widget)
        {
            if (widget.IsRoot) return;
            var (x, y, w, _) = widget.Space;

            var nh = CalculateStretchedHeight(widget);

            if (!widget.Props.Contains<HorizontalStretch>())
                w = widget.Children.Any() ? widget.Children.Max(c => c.Space.Width) : w;

            WidgetsSpaceHelper.UpdateSpace(widget,
                new RectangleF(x, y, w, nh));
            OnApplied();
        }

        private static float CalculateStretchedHeight(IWidget widget)
        {
            var nh = widget.Parent!.Space.Height;

            if (!widget.Parent.Props.SafeGetByProp<Column>().TryGetValue(out var cols)) return nh;
            if (cols!.FirstOrDefault() == null) return nh;

            var siblings = widget.Parent.Children.Where(w => w != widget);
            var count = 1;
            float nonStretchedHeight = 0;
            foreach (var sibling in siblings)
            {
                if (sibling.Props.Contains<VerticalStretch>()) count++;
                else nonStretchedHeight += sibling.Space.Height;
            }

            nh -= nonStretchedHeight;
            return nh / count;
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);
    }
}