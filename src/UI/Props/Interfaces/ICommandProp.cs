using System;

namespace WForest.UI.Props.Interfaces
{
    public interface ICommandProp : IProp
    {

        Action Action { get; set; }
        void Execute() => Action();
    }
}