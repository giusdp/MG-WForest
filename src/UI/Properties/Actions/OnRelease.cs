using System;

namespace WForest.UI.Properties.Actions
{
    public class OnRelease : Property
    {
        private readonly Action _function;

        internal OnRelease(Action onPress)
        {
            _function = onPress;
        }

        public override void ApplyOn(WidgetTrees.WidgetTree widgetNode)
        {
            widgetNode.Data.AddOnRelease(_function);
        }
    }
}