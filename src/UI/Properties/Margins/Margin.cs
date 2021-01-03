using Microsoft.Xna.Framework;
using WForest.UI.Utils;
using WForest.UI.Widgets;
using WForest.UI.WidgetTrees;
using WForest.Utilities;

namespace WForest.UI.Properties.Margins
{
    public class Margin : Property
    {
        internal override int Priority { get; } = 0;

        private readonly Utilities.MarginValues _marginValues;

        internal Margin(int marginLeft, int marginRight, int marginTop, int marginBottom)
        {
            _marginValues = new Utilities.MarginValues(marginLeft, marginRight, marginTop, marginBottom);
        }

        internal override void ApplyOn(WidgetTrees.WidgetTree widgetNode)
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