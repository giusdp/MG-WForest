using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WForest.Utilities;

namespace WForest.UI.Properties.Border
{
    public class Border : IProperty
    {
        public Color Color { get; set; }
        public int LineWidth { get; set; }

        public Border()
        {
            LineWidth = 1;
            Color = Color.Black;
        }

        public int Priority { get; } = 0;

        public void ApplyOn(WidgetTree widgetNode)
        {
            widgetNode.Data.Modifiers.Add(sb => { Primitives.DrawRectangle(sb, widgetNode.Data.Space, Color, LineWidth); });
        }
    }
}