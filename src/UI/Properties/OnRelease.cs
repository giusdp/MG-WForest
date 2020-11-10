using System;

namespace PiBa.UI.Properties
{
    public class OnRelease : IProperty
    {
       private readonly Action _function; 
        
        public OnRelease(Action onPress)
        {
            _function = onPress;
        }
        public void ApplyOn(WidgetTree widgetNode)
        {
            widgetNode.Data.OnRelease = _function;
        } 
    }
}