using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WForest.Utilities;

namespace WForest.UI.Properties.Border
{
    public class Border : Property
    {
        public override int Priority { get; } = 10;
        public Color Color { get; set; }
        public int LineWidth { get; set; }

        internal Border()
        {
            LineWidth = 1;
            Color = Color.Black;
        }

        public override void ApplyOn(WidgetTrees.WidgetTree widgetNode)
        {
            widgetNode.Data.PostDrawing.Add(sb => { Primitives.DrawBorder(sb, widgetNode.Data.Space, Color, LineWidth); });
        }
    }
}