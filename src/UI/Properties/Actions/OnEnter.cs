using System;

namespace WForest.UI.Properties.Actions
{
    public class OnEnter : Property
    {
        private readonly Action _function;

        public OnEnter(Action onPress)
        {
            _function = onPress;
        }

        internal override void ApplyOn(WidgetTree widgetNode)
        {
            widgetNode.Data.OnEnter = _function;
        }
    }
}