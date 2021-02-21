using System;
using WForest.Props.Interfaces;

namespace WForest.Props.Props.Actions
{
    /// <summary>
    /// Command Prop used to execute actions on the Update method of the game.
    /// </summary>
    public class OnUpdate : ICommandProp
    {
        /// <inherit/>
        public Action Action { get; set; }

        public void Execute() => Action();

        /// <summary>
        /// Create an OnUpdate prop with the action that will be executed at every frame.
        /// </summary>
        /// <param name="onUpdate"></param>
        public OnUpdate(Action onUpdate)
        {
            Action = onUpdate;
        }
    }
}