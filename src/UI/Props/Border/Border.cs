using Microsoft.Xna.Framework;
using WForest.UI.Widgets;
using WForest.Utilities;

namespace WForest.UI.Props.Border
{
    /// <summary>
    /// Property that adds a rectangular border to a widget. Customizable with the color and line width.
    /// </summary>
    public class Border : Prop
    {
        /// <summary>
        /// Border is one of the last props.
        /// </summary>
        public override int Priority { get; } = 4;
        internal Color Color { get; set; }
        internal int LineWidth { get; set; }

        internal Border()
        {
            LineWidth = 1;
            Color = Color.Black;
        }

        /// <summary>
        /// Adds a PostDrawing modifier so that the border is drawn on top of the widget.
        /// </summary>
        /// <param name="widget"></param>
        public override void ApplyOn(IWidget widget)
        {
            // widget.WidgetNode.Data.PostDrawing.Add(sb => { Primitives.DrawBorder(sb, widget.WidgetNode.Data.Space, Color, LineWidth); });
        }
    }
}