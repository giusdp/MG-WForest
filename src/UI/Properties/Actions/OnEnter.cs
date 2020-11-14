using System;

namespace PiBa.UI.Properties.Actions
{
    public class OnEnter : IProperty
    {
        public int Priority { get; } = 0;
        private readonly Action _function;

        public OnEnter(Action onPress)
        {
            _function = onPress;
        }

        public void ApplyOn(WidgetTree widgetNode)
        {
            widgetNode.Data.OnEnter = _function;
        }
    }
}