using System;

namespace WForest.UI.Properties
{
    public abstract class Property
    {
        public event EventHandler Applied;

        internal virtual int Priority { get; } = 0;
        internal abstract void ApplyOn(WidgetTrees.WidgetTree widgetNode);

        internal void ApplyOnAndFireApplied(WidgetTrees.WidgetTree widgetNode)
        {
            ApplyOn(widgetNode);
            OnApplied();
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);
    }
}