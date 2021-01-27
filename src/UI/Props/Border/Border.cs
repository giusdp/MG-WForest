using System;
using Microsoft.Xna.Framework;
using WForest.UI.Props.Interfaces;
using WForest.UI.Widgets.Interfaces;

namespace WForest.UI.Props.Border
{
    /// <summary>
    /// Property that adds a rectangular border to a widget. Customizable with the color and line width.
    /// </summary>
    public class Border : IApplicableProp
    {
        /// <summary>
        /// Border is one of the last props.
        /// </summary>
        public int Priority { get; set; } = 4;

        public event EventHandler? Applied;
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
        public void ApplyOn(IWidget widget)
        {
            // widget.WidgetNode.Data.PostDrawing.Add(sb => { Primitives.DrawBorder(sb, widget.WidgetNode.Data.Space, Color, LineWidth); });
            OnApplied();
        }

        protected virtual void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);
    }
}