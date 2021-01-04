using System;

namespace WForest.UI.Properties
{
    public abstract class Property
    {
        public event EventHandler Applied;

        public virtual int Priority { get; } = 0;
        public abstract void ApplyOn(WidgetTrees.WidgetTree widgetNode);

        internal void ApplyOnAndFireApplied(WidgetTrees.WidgetTree widgetNode)
        {
            ApplyOn(widgetNode);
            OnApplied();
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);
    }
}