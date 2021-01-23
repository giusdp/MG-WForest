using Microsoft.Xna.Framework;

namespace WForest.UI.Props
{
    /// <summary>
    /// Property to change color used when drawing.
    /// </summary>
    public class ColorProp : Prop
    {
        private readonly Color _color;

        internal ColorProp(Color color)
        {
            _color = color;
        }

        /// <summary>
        /// Changed the Color field of the widget with the new color passed to this property constructor.
        /// </summary>
        /// <param name="widgetNode"></param>
        public override void ApplyOn(WidgetTrees.WidgetTree widgetNode)
        {
            widgetNode.Data.Color = _color;
        }
    }
}