using System;

namespace PiBa.UI.Properties
{
    public class OnExit : IProperty
    {
       private readonly Action _function; 
        
        public OnExit(Action onPress)
        {
            _function = onPress;
        }
        public void ApplyOn(WidgetTree widgetNode)
        {
            widgetNode.Data.OnExit = _function;
        } 
    }
}