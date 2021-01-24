using Microsoft.Xna.Framework;
using WForest.UI.Utils;
using WForest.UI.Widgets;
using WForest.Utilities;

namespace WForest.UI.Props
{
    /// <summary>
    /// Margin property that adds margin space on a widget on all four directions.
    /// </summary>
    public class Margin : Prop
    {
        /// <summary>
        /// Margin should be one of the first to be applied.
        /// </summary>
        public override int Priority { get; } = 0;

        private readonly MarginValues _marginValues;

        internal Margin(int marginLeft, int marginRight, int marginTop, int marginBottom)
        {
            _marginValues = new MarginValues(marginLeft, marginRight, marginTop, marginBottom);
        }

        /// <summary>
        /// Adds margin space to the widget. It updates the space required by the widget and applies the changes to all the children.
        /// </summary>
        /// <param name="widget"></param>
        public override void ApplyOn(IWidget widget)
        {
            var (x, y, w, h) = widget.Space;
            widget.Margins = AddMargin(widget);

            var newSpace = new Rectangle(x + _marginValues.Left, y + _marginValues.Top, w, h);
            WidgetsSpaceHelper.UpdateSpace(widget, newSpace);

            // TreeVisitor.ApplyToTreeLevelByLevel(
            //     widget,
            //     lvl => lvl.ForEach(node =>
            //         WidgetsSpaceHelper.UpdateSpace(node, new Rectangle(widget.Space.Location, node.Data.Space.Size))));
        }

        private MarginValues AddMargin(IWidget widget)
        {
            var m = widget.Margins;
            return new MarginValues(m.Left + _marginValues.Left, m.Right + _marginValues.Right,
                m.Top + _marginValues.Top,
                m.Bottom + _marginValues.Bottom);
        }
    }
}