using System;
using WForest.Props.Interfaces;

namespace WForest.Props.Props.Actions
{
    /// <summary>
    /// Property that adds an action on the widget. When the button press on a widget is released (while still staying on the same widget)
    /// all the OnRelease actions are triggered.
    /// </summary>
    public class OnRelease : ICommandProp
    {
        /// <inherit/>
        public Action Action { get; set; }

        public void Execute() => Action();
        public OnRelease(Action onPress)
        {
            Action = onPress;
        }
    }
}