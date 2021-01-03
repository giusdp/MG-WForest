using System;

namespace WForest.UI.Properties.Actions
{
    public class OnExit : Property
    {
        private readonly Action _function;

        internal OnExit(Action onPress)
        {
            _function = onPress;
        }

        internal override void ApplyOn(WidgetTrees.WidgetTree widgetNode)
        {
            widgetNode.Data.AddOnExit(_function);
        }
    }
}