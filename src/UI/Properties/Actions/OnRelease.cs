using System;

namespace WForest.UI.Properties.Actions
{
    public class OnRelease : IProperty
    {
        public int Priority { get; } = 0;
        private readonly Action _function;

        public OnRelease(Action onPress)
        {
            _function = onPress;
        }

        public void ApplyOn(WidgetTree widgetNode)
        {
            widgetNode.Data.OnRelease = _function;
        }
    }
}