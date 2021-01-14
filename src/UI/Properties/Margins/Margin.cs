using Microsoft.Xna.Framework;
using WForest.UI.Utils;
using WForest.UI.Widgets;
using WForest.UI.WidgetTrees;
using WForest.Utilities;

namespace WForest.UI.Properties.Margins
{
    
    /// <summary>
    /// Margin property that adds margin space on a widget on all four directions.
    /// </summary>
    public class Margin : Property
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
        /// <param name="widgetNode"></param>
        public override void ApplyOn(WidgetTree widgetNode)
        {
            var (x, y, w, h) = widgetNode.Data.Space;
            widgetNode.Data.MarginValues = _marginValues;
            WidgetsSpaceHelper.UpdateSpace(widgetNode, new Rectangle(x + _marginValues.Left, y + _marginValues.Top, w, h));
            TreeVisitor<Widget>.ApplyToTreeLevelByLevel(
                widgetNode,
                lvl =>
                    lvl.ForEach(node =>
                        WidgetsSpaceHelper.UpdateSpace((WidgetTree) node,
                            new Rectangle(widgetNode.Data.Space.Location, node.Data.Space.Size))));
        }
    }
}