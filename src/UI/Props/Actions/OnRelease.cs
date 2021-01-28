using System;
using WForest.UI.Props.Interfaces;
using WForest.UI.Widgets.Interfaces;

namespace WForest.UI.Props.Actions
{
    /// <summary>
    /// Property that adds an action on the widget. When the button press on a widget is released (while still staying on the same widget)
    /// all the OnRelease actions are triggered.
    /// </summary>
    public class OnRelease : ICommandProp
    {
        /// <inherit/>
        public Action Action { get; set; }

        internal OnRelease(Action onPress)
        {
            Action = onPress;
        }
    }
}