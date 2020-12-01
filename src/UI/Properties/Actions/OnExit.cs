using System;

namespace WForest.UI.Properties.Actions
{
    public class OnExit : Property
    {
        private readonly Action _function;

        public OnExit(Action onPress)
        {
            _function = onPress;
        }

        internal override void ApplyOn(WidgetTree widgetNode)
        {
            widgetNode.Data.OnExit = _function;
        }
    }
}