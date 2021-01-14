using System;

namespace WForest.UI.Properties.Actions
{
    /// <summary>
    /// Property that adds an action on the widget. When a cursor exits the widget space all the OnExit actions are
    /// triggered.
    /// </summary>
    public class OnExit : Property
    {
        private readonly Action _function;

        internal OnExit(Action onPress)
        {
            _function = onPress;
        }

        /// <summary>
        /// Adds this OnExit action to the widget.
        /// </summary>
        /// <param name="widgetNode"></param>
        public override void ApplyOn(WidgetTrees.WidgetTree widgetNode)
        {
            widgetNode.Data.AddOnExit(_function);
        }
    }
}