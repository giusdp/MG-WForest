using System;

namespace WForest.UI.Props.Interfaces
{
    /// <summary>
    /// Prop that holds some function that can be executed at any time.
    /// </summary>
    public interface ICommandProp : IProp
    {
        /// <summary>
        /// The action the command holds.
        /// </summary>
        Action Action { get; set; }

        /// <summary>
        /// Execute the command.
        /// </summary>
        void Execute() => Action();
    }
}