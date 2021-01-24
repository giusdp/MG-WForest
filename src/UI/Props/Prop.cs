using System;
using WForest.UI.Widgets;

namespace WForest.UI.Props
{

    /// <summary>
    /// Base class for properties to apply on widgets. Inherit this class to create custom props.
    /// </summary>
    public abstract class Prop
    {
        /// <summary>
        /// Event fired when this property finished applying on a widget.
        /// </summary>
        public event EventHandler? Applied;

        /// <summary>
        /// Priority of application on widget. It's used to sort the order of applications.
        /// </summary>
        public virtual int Priority { get; } = 0;
        
        /// <summary>
        /// Apply the prop on a widget.
        /// </summary>
        /// <param name="widget"></param>
        public abstract void ApplyOn(IWidget widget);

        internal void ApplyOnAndFireApplied(IWidget widget)
        {
            ApplyOn(widget);
            OnApplied();
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);
    }
}