using System;
using System.Collections.Generic;
using WForest.UI.Widgets.Interfaces;

namespace WForest.UI.Interactions
{
    public class InteractionRunner
    {
        public virtual void ChangeState(IWidget interactiveObj, Interaction interaction)
        {
            switch (interactiveObj.CurrentInteraction)
            {
                // case Interaction.Untouched:
                //     if (interaction is Interaction.Entered) 
                //         interactiveObj.CurrentInteraction = interaction;
                //     break;
                // case Interaction.Entered:
                //     if (interaction is Interaction.Exited){}
                //     // PressOrExit(interaction);
                //     break;
                // case Interaction.Pressed:
                //     // ReleasedOrExited(interaction);
                //     break;
            }
        }
        
        private void RunInteractionProps(Interaction interaction){}

        //
        // internal void Update()
        // {
        //     switch (CurrentInteraction)
        //     {
        //         case Interaction.Untouched:
        //             break;
        //         case Interaction.Entered:
        //             Exec(OnEnter);
        //             break;
        //         case Interaction.Exited:
        //             Exec(OnExit);
        //             break;
        //         case Interaction.Pressed:
        //             Exec(OnPress);
        //             break;
        //         case Interaction.Released:
        //             Exec(OnRelease);
        //             break;
        //         default:
        //             throw new ArgumentOutOfRangeException();
        //     }
        // }
        //
        // #region Utils
        //
        // private static void Exec(List<Action> actions) => actions.ForEach(a => a());
        //
        // private void EnterIfEntered(Interaction interaction)
        // {
        //     if (!(interaction is Interaction.Entered)) return;
        //     ExecAndUpdateCurrent(OnEnter, interaction);
        // }
        //
        // private void PressOrExit(Interaction interaction)
        // {
        //     switch (interaction)
        //     {
        //         case Interaction.Exited:
        //             ExecAndUpdateCurrent(OnExit, Interaction.Untouched);
        //             break;
        //         case Interaction.Pressed:
        //             ExecAndUpdateCurrent(OnPress, interaction);
        //             break;
        //     }
        // }
        //
        // private void ReleasedOrExited(Interaction interaction)
        // {
        //     switch (interaction)
        //     {
        //         case Interaction.Released:
        //             ExecAndUpdateCurrent(OnRelease, Interaction.Entered);
        //             break;
        //         case Interaction.Exited:
        //             ExecAndUpdateCurrent(OnExit, Interaction.Untouched);
        //             break;
        //     }
        // }
        //
        // private void ExecAndUpdateCurrent(List<Action> actions, Interaction i)
        // {
        //     Exec(actions);
        //     CurrentInteraction = i;
        // }
        //
        // #endregion
    }
}