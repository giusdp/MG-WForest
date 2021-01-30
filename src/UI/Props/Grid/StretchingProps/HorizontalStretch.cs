using System;
using System.Linq;
using Microsoft.Xna.Framework;
using WForest.UI.Props.Interfaces;
using WForest.UI.Utils;
using WForest.UI.Widgets.Interfaces;

namespace WForest.UI.Props.Grid.StretchingProps
{
    /// <summary>
    /// Property to increase the width of the widget til the width of the parent.
    /// If the widget is the root, it does nothing since it has no parent.
    /// </summary>
    public class HorizontalStretch : IApplicableProp
    {
        /// <inheritdoc/>
        public int Priority { get; set; }

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
            var nw = widget.Parent!.Space.Width -
                     widget.Parent!.Children.Where(w => w != widget).Sum(w => w.Space.Width);
            WidgetsSpaceHelper.UpdateSpace(widget,
                new Rectangle(x, y, nw, h));
            OnApplied();
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);
    }
}