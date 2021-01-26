using System;
using Microsoft.Xna.Framework;
using WForest.UI.Props.Interfaces;
using WForest.UI.Utils;
using WForest.UI.Widgets.Interfaces;

namespace WForest.UI.Props.Grid.StretchingProps
{
    /// <summary>
    /// Property to increase the height of the widget til the height of the parent.
    /// If the widget is the root, it does nothing since it has no parent.
    /// </summary>
    public class VerticalStretch : IApplicableProp
    {
        internal VerticalStretch(){}
        public int Priority { get; set; }
        public event EventHandler? Applied;

        /// <summary>
        /// It gets the height of the parent (if it has one) and replaces the widget's height with it, then updates the spaces of its children.
        /// </summary>
        /// <param name="widget"></param>
        public void ApplyOn(IWidget widget)
        {
            if (widget.IsRoot) return;
            var (x, y, w, _) = widget.Space;
            WidgetsSpaceHelper.UpdateSpace(widget,
                new Rectangle(x, y, w, widget.Parent!.Space.Height));
        }
    }
}