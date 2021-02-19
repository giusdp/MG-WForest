using System;
using Microsoft.Xna.Framework;
using WForest.Props.Interfaces;
using WForest.Utilities;
using WForest.Widgets.Interfaces;

namespace WForest.Props
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

        /// <inheritdoc/>
        public event EventHandler? Applied;

        internal Color Color { get; set; }
        internal int LineWidth { get; set; }

        /// <inheritdoc/>
        public bool IsApplied { get; set; }

        public Border()
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
            IsApplied = false;
            widget.PostDrawActions.Add(r => r.DrawBorder(widget.Space, Color, LineWidth));
            IsApplied = true;
            OnApplied();
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);
    }
}