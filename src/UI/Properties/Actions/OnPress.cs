using System;

namespace WForest.UI.Properties.Actions
{
    public class OnPress : Property
    {
        private readonly Action _function; 
        
        internal OnPress(Action onPress)
        {
            _function = onPress;
        }

        public override void ApplyOn(WidgetTrees.WidgetTree widgetNode)
        {
            widgetNode.Data.AddOnPressed(_function);
        }
    }
}