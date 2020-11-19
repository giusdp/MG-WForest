using System;

namespace WForest.UI.Properties.Actions
{
    public class OnExit : IProperty
    {
        public int Priority { get; } = 0;
        private readonly Action _function;

        public OnExit(Action onPress)
        {
            _function = onPress;
        }

        public void ApplyOn(WidgetTree widgetNode)
        {
            widgetNode.Data.OnExit = _function;
        }
    }
}