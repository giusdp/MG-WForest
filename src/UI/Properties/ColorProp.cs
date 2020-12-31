using Microsoft.Xna.Framework;

namespace WForest.UI.Properties
{
    public class ColorProp : Property
    {
        private readonly Color _color;

        internal ColorProp(Color color)
        {
            _color = color;
        }

        internal override void ApplyOn(WidgetTree.WidgetTree widgetNode)
        {
            widgetNode.Data.Color = _color;
        }
    }
}