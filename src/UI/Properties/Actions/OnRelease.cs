using System;

namespace WForest.UI.Properties.Actions
{
    public class OnRelease : Property
    {
        private readonly Action _function;

        public OnRelease(Action onPress)
        {
            _function = onPress;
        }

        internal override void ApplyOn(WidgetTree.WidgetTree widgetNode)
        {
            widgetNode.Data.AddOnRelease(_function);
        }
    }
}