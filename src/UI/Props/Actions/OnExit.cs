using System;
using WForest.UI.Widgets;

namespace WForest.UI.Props.Actions
{
    /// <summary>
    /// Property that adds an action on the widget. When a cursor exits the widget space all the OnExit actions are
    /// triggered.
    /// </summary>
    public class OnExit : Prop
    {
        private readonly Action _function;

        internal OnExit(Action onPress)
        {
            _function = onPress;
        }

        /// <summary>
        /// Adds this OnExit action to the widget.
        /// </summary>
        /// <param name="widget"></param>
        public override void ApplyOn(IWidget widget)
        {
            // widget.WidgetNode.Data.AddOnExit(_function);
        }
    }
}