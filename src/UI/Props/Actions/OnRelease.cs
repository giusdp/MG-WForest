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
        private readonly Action _function;

        internal OnRelease(Action onPress)
        {
            _function = onPress;
        }

        public void Execute() => _function();
    }
}