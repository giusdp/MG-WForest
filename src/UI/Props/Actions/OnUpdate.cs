using System;
using WForest.UI.Props.Interfaces;

namespace WForest.UI.Props.Actions
{
    public class OnUpdate : ICommandProp
    {
        public Action Action { get; set; }

        public OnUpdate(Action onUpdate)
        {
            Action = onUpdate;
        }
    }
}