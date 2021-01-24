using System;
using WForest.UI.Widgets;

namespace WForest.UI.Props.Actions
{
    /// <summary>
    /// Property that adds an action on the widget. When the widget is pressed (like a click on the mouse) all the OnPress actions are
    /// triggered.
    /// </summary>
    public class OnPress : Prop
    {
        private readonly Action _function;

        internal OnPress(Action onPress)
        {
            _function = onPress;
        }

        /// <summary>
        /// Adds this OnPress action to the widget.
        /// </summary>
        /// <param name="widget"></param>
        public override void ApplyOn(IWidget widget)
        {
            // widget.WidgetNode.Data.AddOnPressed(_function);
        }
    }
}