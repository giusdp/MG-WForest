using Microsoft.Xna.Framework;
using WForest.UI.Widgets;
using WForest.Utilities;

namespace WForest.UI.Properties.Margins
{
    public class Margin : IProperty
    {
        public int Priority { get; } = 0;
        private readonly Utilities.Margin _margin;

        public Margin(int marginLeft, int marginRight, int marginTop, int marginBottom)
        {
            _margin =  new Utilities.Margin(marginLeft, marginRight, marginTop, marginBottom);
        }

        public void ApplyOn(WidgetTree widgetNode)
        {
            var (x, y, w, h) = widgetNode.Data.Space;
            widgetNode.Data.Space = new Rectangle(x + _margin.Left, y + _margin.Top, w, h);
            widgetNode.Data.Margin = _margin;
            TreeVisitor<Widget>.ApplyToTreeFromRoot(
                widgetNode,
                node => 
                    node.Data.Space = new Rectangle(widgetNode.Data.Space.Location, node.Data.Space.Size));
        }
    }
}