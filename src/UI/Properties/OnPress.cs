using System;

namespace PiBa.UI.Properties
{
    public class OnPress : IProperty
    {
        private readonly Action _function; 
        
        public OnPress(Action onPress)
        {
            _function = onPress;
        }
        public void ApplyOn(WidgetTree widgetNode)
        {
            widgetNode.Data.OnPress = _function;
        }
    }
}