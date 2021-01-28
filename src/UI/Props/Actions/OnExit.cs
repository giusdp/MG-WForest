using System;
using WForest.UI.Props.Interfaces;
using WForest.UI.Widgets.Interfaces;

namespace WForest.UI.Props.Actions
{
    /// <summary>
    /// Property that adds an action on the widget. When a cursor exits the widget space all the OnExit actions are
    /// triggered.
    /// </summary>
    public class OnExit : ICommandProp
    {
        public Action Action { get; set; }

        internal OnExit(Action onPress)
        {
            Action = onPress;
        }
    }
}