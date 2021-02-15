using System;
using System.Linq;
using WForest.Props.Interfaces;
using WForest.UI.Props.Grid.Utils;
using WForest.Utilities;
using WForest.Utilities.WidgetUtils;
using WForest.Widgets.Interfaces;

namespace WForest.Props.Grid.StretchingProps
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

        /// <inheritdoc/>
        public bool ApplicationDone { get; set; }

        /// <summary>
        /// It gets the height of the parent (if it has one) and replaces the widget's height with it, then updates the spaces of its children.
        /// </summary>
        /// <param name="widget"></param>
        public void ApplyOn(IWidget widget)
        {
            ApplicationDone = false;
            if (widget.IsRoot) return;
            var (x, y, w, _) = widget.Space;
            if (!widget.Props.Contains<HorizontalStretch>())
                w = widget.Children.Any() ? widget.Children.Max(c => c.Space.Width) : w;
            
            var (nh, nonFinishedHorSiblings) = ApplyUtils.StretchedHeightUsingHeightSiblings(widget);
            nonFinishedHorSiblings.ForEach(s =>
            {
                var hp = (IApplicableProp) s.Props.GetByProp<HorizontalStretch>().First();
                hp.Applied += (_, _) =>
                {
                    ApplicationDone = false;
                    WidgetsSpaceHelper.UpdateSpace(widget, new RectangleF(x, y, w, nh - s.Space.Height));
                    ApplicationDone = true;
                };
            });

            WidgetsSpaceHelper.UpdateSpace(widget, new RectangleF(x, y, w, nh));
            ApplicationDone = nonFinishedHorSiblings.Count == 0;
            OnApplied();
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);
    }
}