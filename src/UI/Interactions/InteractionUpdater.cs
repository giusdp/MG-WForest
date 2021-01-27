using System.Collections.Generic;
using System.Linq;
using WForest.UI.Props.Actions;
using WForest.UI.Props.Interfaces;
using WForest.UI.Widgets.Interfaces;
using WForest.Utilities.Collections;

namespace WForest.UI.Interactions
{
    public class InteractionUpdater
    {
        public virtual IEnumerable<ICommandProp> NextState(IWidget interactedObj, Interaction interaction)
        {
            var (nextInteraction, transitionInteraction) = GetNextAndTransitionInteraction(interactedObj, interaction);
            interactedObj.CurrentInteraction = nextInteraction;
            return GetPropsOfInteraction(interactedObj.Props, transitionInteraction);
        }

        private static (Interaction, Interaction) GetNextAndTransitionInteraction(IWidget interacted,
            Interaction interaction)
        {
            return interacted.CurrentInteraction switch
            {
                Interaction.Untouched => HandleUntouchedCase(interaction),
                Interaction.Entered => HandleEnteredCase(interaction),
                Interaction.Pressed => HandlePressedCase(interaction),
                _ => (Interaction.Untouched, Interaction.Untouched)
            };
        }

        private static (Interaction, Interaction) HandleUntouchedCase(Interaction interaction)
        {
            return interaction switch
            {
                Interaction.Entered => (Interaction.Entered, Interaction.Entered),
                Interaction.Pressed => (Interaction.Pressed, Interaction.Pressed),
                _ => (Interaction.Untouched, Interaction.Untouched)
            };
        }

        private static (Interaction, Interaction) HandleEnteredCase(Interaction interaction)
        {
            return interaction switch
            {
                Interaction.Exited => (Interaction.Untouched, Interaction.Exited),
                Interaction.Pressed => (Interaction.Pressed, Interaction.Pressed),
                Interaction.Untouched => (Interaction.Untouched, Interaction.Untouched),
                _ => (Interaction.Entered, Interaction.Untouched)
            };
        }

        private static (Interaction, Interaction) HandlePressedCase(Interaction interaction)
        {
            return interaction switch
            {
                Interaction.Exited => (Interaction.Untouched, Interaction.Exited),
                Interaction.Released => (Interaction.Entered, Interaction.Released),
                _ => (Interaction.Pressed, Interaction.Untouched)
            };
        }


        private IEnumerable<ICommandProp> GetPropsOfInteraction(PropCollection props, Interaction interaction)
        {
            var l = new List<IProp>();
            var b = interaction switch
            {
                Interaction.Entered => props.TryGetPropValue<OnEnter>(out l),
                Interaction.Exited => props.TryGetPropValue<OnExit>(out l),
                Interaction.Pressed => props.TryGetPropValue<OnPress>(out l),
                Interaction.Released => props.TryGetPropValue<OnRelease>(out l),
                _ => false
            };
            if (b && l != null) return l.Cast<ICommandProp>();
            return new List<ICommandProp>();
        }

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