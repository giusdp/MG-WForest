using System;
using Microsoft.Xna.Framework;
using WForest.Props.Interfaces;
using WForest.Widgets.Interfaces;

namespace WForest.Props.Props
{
    /// <summary>
    /// Property to change color used when drawing.
    /// </summary>
    public class ColorProp : IApplicableProp
    {
        /// <inheritdoc/>
        public int Priority { get; set; }

        /// <inheritdoc/>
        public event EventHandler? Applied;

        /// <inheritdoc/>
        public bool IsApplied { get; set; }

        private readonly Color _color;

        public ColorProp(Color color)
        {
            _color = color;
        }

        /// <summary>
        /// Changed the Color field of the widget with the new color passed to this property constructor.
        /// </summary>
        /// <param name="widget"></param>
        public void ApplyOn(IWidget widget)
        {
            IsApplied = false;
            widget.Color = _color;
            IsApplied = true;
            OnApplied();
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);
    }
}