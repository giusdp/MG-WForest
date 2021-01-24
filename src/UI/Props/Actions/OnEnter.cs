using System;
using WForest.UI.Widgets;

namespace WForest.UI.Props.Actions
{
    /// <summary>
    /// Property that adds an action on the widget. When a cursor enters the widget space all the OnEnter actions are
    /// triggered.
    /// </summary>
    public class OnEnter : Prop
    {
        private readonly Action _function;

        internal OnEnter(Action onPress)
        {
            _function = onPress;
        }

        /// <summary>
        /// Adds this OnEnter action to the widget.
        /// </summary>
        /// <param name="widget"></param>
        public override void ApplyOn(IWidget widget)
        {
            // widget.WidgetNode.Data.AddOnEnter(_function);
        }
    }
}