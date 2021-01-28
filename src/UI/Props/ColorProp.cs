using System;
using Microsoft.Xna.Framework;
using WForest.UI.Props.Interfaces;
using WForest.UI.Widgets.Interfaces;

namespace WForest.UI.Props
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

        private readonly Color _color;

        internal ColorProp(Color color)
        {
            _color = color;
        }

        /// <summary>
        /// Changed the Color field of the widget with the new color passed to this property constructor.
        /// </summary>
        /// <param name="widget"></param>
        public void ApplyOn(IWidget widget)
        {
            widget.Color = _color;
            OnApplied();
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);
    }
}