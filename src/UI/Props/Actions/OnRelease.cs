using System;

namespace WForest.UI.Props.Actions
{
    /// <summary>
    /// Property that adds an action on the widget. When the button press on a widget is released (while still staying on the same widget)
    /// all the OnRelease actions are triggered.
    /// </summary>
    public class OnRelease : Prop
    {
        private readonly Action _function;

        internal OnRelease(Action onPress)
        {
            _function = onPress;
        }

        /// <summary>
        /// Adds this OnRelease action to the widget.
        /// </summary>
        /// <param name="widgetNode"></param>
        public override void ApplyOn(WidgetTrees.WidgetTree widgetNode)
        {
            widgetNode.Data.AddOnRelease(_function);
        }
    }
}