using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WForest.Utilities;

namespace WForest.UI.Properties.Border
{
    public class Border : Property
    {
        public Color Color { get; set; }
        public int LineWidth { get; set; }

        public Border()
        {
            LineWidth = 1;
            Color = Color.Black;
        }

        internal override void ApplyOn(WidgetTree.WidgetTree widgetNode)
        {
            widgetNode.Data.Modifiers.Add(sb => { Primitives.DrawBorder(sb, widgetNode.Data.Space, Color, LineWidth); });
        }
    }
}