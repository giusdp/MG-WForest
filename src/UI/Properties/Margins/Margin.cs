using Microsoft.Xna.Framework;
using WForest.UI.Utils;
using WForest.UI.Widgets;
using WForest.Utilities;

namespace WForest.UI.Properties.Margins
{
    public class Margin : Property
    {
        internal override int Priority { get; } = 0;

        private readonly Utilities.Margin _margin;

        internal Margin(int marginLeft, int marginRight, int marginTop, int marginBottom)
        {
            _margin = new Utilities.Margin(marginLeft, marginRight, marginTop, marginBottom);
        }

        internal override void ApplyOn(WidgetTree.WidgetTree widgetNode)
        {
            var (x, y, w, h) = widgetNode.Data.Space;
            widgetNode.Data.Margin = _margin;
            WidgetsSpaceHelper.UpdateSpace(widgetNode, new Rectangle(x + _margin.Left, y + _margin.Top, w, h));
            TreeVisitor<Widget>.ApplyToTreeLevelByLevel(
                widgetNode,
                lvl =>
                    lvl.ForEach(node =>
                        WidgetsSpaceHelper.UpdateSpace(node,
                            new Rectangle(widgetNode.Data.Space.Location, node.Data.Space.Size))));
        }
    }
}