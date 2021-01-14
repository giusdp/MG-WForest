using System;

namespace WForest.UI.Properties.Actions
{
    /// <summary>
    /// Property that adds an action on the widget. When a cursor enters the widget space all the OnEnter actions are
    /// triggered.
    /// </summary>
    public class OnEnter : Property
    {
        private readonly Action _function;

        internal OnEnter(Action onPress)
        {
            _function = onPress;
        }

        /// <summary>
        /// Adds this OnEnter action to the widget.
        /// </summary>
        /// <param name="widgetNode"></param>
        public override void ApplyOn(WidgetTrees.WidgetTree widgetNode)
        {
            widgetNode.Data.AddOnEnter(_function);
        }
    }
}