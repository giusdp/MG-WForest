using Microsoft.Xna.Framework;
using WForest.UI.Utils;
using WForest.UI.Widgets;

namespace WForest.UI.Props.Grid.StretchingProps
{
    /// <summary>
    /// Property to increase the width of the widget til the width of the parent.
    /// If the widget is the root, it does nothing since it has no parent.
    /// </summary>
    public class HorizontalStretch : Prop
    {
        internal HorizontalStretch()
        {
        }

        /// <summary>
        /// It gets the width of the parent (if it has one) and replaces the widget's width with it, then updates the spaces of its children.
        /// </summary>
        /// <param name="widget"></param>
        public override void ApplyOn(IWidget widget)
        {
            if (widget.IsRoot) return;
            var (x, y, _, h) = widget.Space;
            WidgetsSpaceHelper.UpdateSpace(widget,
                new Rectangle(x, y, widget.Parent!.Space.Width, h));
        }
    }
}