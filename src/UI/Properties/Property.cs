using System;

namespace WForest.UI.Properties
{
    /// <summary>
    /// Base class for properties to apply on widgets. Inherit this class to create custom props.
    /// </summary>
    public abstract class Property
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
        /// Abstract method used to apply modifications (the property) on a widget.
        /// </summary>
        /// <param name="widgetNode"></param>
        public abstract void ApplyOn(WidgetTrees.WidgetTree widgetNode);

        internal void ApplyOnAndFireApplied(WidgetTrees.WidgetTree widgetNode)
        {
            ApplyOn(widgetNode);
            OnApplied();
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);
    }
}