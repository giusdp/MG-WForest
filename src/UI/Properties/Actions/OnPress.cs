using System;

namespace PiBa.UI.Properties.Actions
{
    public class OnPress : IProperty
    {
        public int Priority { get; } = 0;
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