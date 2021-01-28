using System;
using WForest.UI.Props.Interfaces;
using WForest.UI.Widgets.Interfaces;

namespace WForest.UI.Props.Actions
{
    /// <summary>
    /// Property that adds an action on the widget. When a cursor enters the widget space all the OnEnter actions are
    /// triggered.
    /// </summary>
    public class OnEnter : ICommandProp
    {
        public Action Action { get; set; }

        internal OnEnter(Action onPress)
        {
            Action = onPress;
        }
    }
}