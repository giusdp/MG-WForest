using System.Collections.Generic;
using WForest.Props.Collections;
using WForest.Props.Interfaces;
using WForest.Props.Props.Actions;
using WForest.Utilities;
using WForest.Widgets.Interfaces;

namespace WForest.Interactions
{
    public class DefaultInteractionUpdater : IInteractionUpdater
    {
        public IEnumerable<ICommandProp> NextState(IWidget interactedObj, Interaction interaction)
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


        private static IEnumerable<ICommandProp> GetPropsOfInteraction(IPropCollection props, Interaction interaction)
        {
            Maybe<IEnumerable<ICommandProp>> maybeProps = interaction switch
            {
                Interaction.Entered => props.SafeGet<OnEnter>().Bind(Maybe.Some<IEnumerable<ICommandProp>>),
                Interaction.Exited => props.SafeGet<OnExit>().Bind(Maybe.Some<IEnumerable<ICommandProp>>),
                Interaction.Pressed => props.SafeGet<OnPress>().Bind(Maybe.Some<IEnumerable<ICommandProp>>),
                Interaction.Released => props.SafeGet<OnRelease>().Bind(Maybe.Some<IEnumerable<ICommandProp>>),
                _ => Maybe.None
            };
            return maybeProps switch
            {
                Maybe<IEnumerable<ICommandProp>>.Some l => l.Value,
                _ => new List<ICommandProp>()
            };
        }
    }
}