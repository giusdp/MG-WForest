using System;
using WForest.Props.Interfaces;

namespace WForest.Props.Actions
{
    /// <summary>
    /// Property that adds an action on the widget. When the widget is pressed (like a click on the mouse) all the OnPress actions are
    /// triggered.
    /// </summary>
    public class OnPress : ICommandProp
    {
        /// <inherit/>
        public Action Action { get; set; }

        public OnPress(Action onPress)
        {
            Action = onPress;
        }
    }
}