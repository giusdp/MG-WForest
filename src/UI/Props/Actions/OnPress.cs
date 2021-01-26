using System;
using WForest.UI.Props.Interfaces;
using WForest.UI.Widgets.Interfaces;

namespace WForest.UI.Props.Actions
{
    /// <summary>
    /// Property that adds an action on the widget. When the widget is pressed (like a click on the mouse) all the OnPress actions are
    /// triggered.
    /// </summary>
    public class OnPress : ICommandProp
    {
        private readonly Action _function;

        internal OnPress(Action onPress)
        {
            _function = onPress;
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}