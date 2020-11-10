using System;

namespace PiBa.UI.Properties
{
    public class OnEnter : IProperty
    {
       private readonly Action _function; 
        
        public OnEnter(Action onPress)
        {
            _function = onPress;
        }
        public void ApplyOn(WidgetTree widgetNode)
        {
            widgetNode.Data.OnEnter = _function;
        } 
    }
}