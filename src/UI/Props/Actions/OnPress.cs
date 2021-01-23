using System;

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
        /// <param name="widgetNode"></param>
        public override void ApplyOn(WidgetTrees.WidgetTree widgetNode)
        {
            widgetNode.Data.AddOnPressed(_function);
        }
    }
}