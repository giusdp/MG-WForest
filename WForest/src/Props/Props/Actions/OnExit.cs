using System;
using WForest.Props.Interfaces;

namespace WForest.Props.Props.Actions
{
    /// <summary>
    /// Property that adds an action on the widget. When a cursor exits the widget space all the OnExit actions are
    /// triggered.
    /// </summary>
    public class OnExit : ICommandProp
    {
        /// <inherit/>
        public Action Action { get; set; }

        public void Execute() => Action();

        public OnExit(Action onPress)
        {
            Action = onPress;
        }
    }
}