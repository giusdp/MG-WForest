using System;

namespace WForest.UI.Properties.Actions
{
    public class OnEnter : Property
    {
        private readonly Action _function;

        internal OnEnter(Action onPress)
        {
            _function = onPress;
        }

        public override void ApplyOn(WidgetTrees.WidgetTree widgetNode)
        {
            widgetNode.Data.AddOnEnter(_function);
        }
    }
}