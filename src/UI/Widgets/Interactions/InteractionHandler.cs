using System;
using System.Collections.Generic;

namespace WForest.UI.Widgets.Interactions
{
    public class InteractionHandler
    {
        public List<Action> OnEnter { get; }
        public List<Action> OnExit { get; }
        public List<Action> OnPress { get; }
        public List<Action> OnRelease { get; }

        public Interaction CurrentInteraction;

        public InteractionHandler()
        {
            OnEnter = new List<Action>();
            OnExit = new List<Action>();
            OnPress = new List<Action>();
            OnRelease = new List<Action>();
            CurrentInteraction = Interaction.Untouched;
        }

        public void ChangeState(Interaction interaction)
        {
            switch (CurrentInteraction)
            {
                case Interaction.Untouched:
                    EnterIfEntered(interaction);
                    break;
                case Interaction.Entered:
                    PressOrExit(interaction);
                    break;
                case Interaction.Pressed:
                    ReleasedOrExited(interaction);
                    break;
            }
        }

        public void Update()
        {
            switch (CurrentInteraction)
            {
                case Interaction.Untouched:
                    break;
                case Interaction.Entered:
                    Exec(OnEnter);
                    break;
                case Interaction.Exited:
                    Exec(OnExit);
                    break;
                case Interaction.Pressed:
                    Exec(OnPress);
                    break;
                case Interaction.Released:
                    Exec(OnRelease);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #region Utils

        private static void Exec(List<Action> actions) => actions.ForEach(a => a());

        private void EnterIfEntered(Interaction interaction)
        {
            if (!(interaction is Interaction.Entered)) return;
            ExecAndUpdateCurrent(OnEnter, interaction);
        }

        private void PressOrExit(Interaction interaction)
        {
            switch (interaction)
            {
                case Interaction.Exited:
                    ExecAndUpdateCurrent(OnExit, Interaction.Untouched);
                    break;
                case Interaction.Pressed:
                    ExecAndUpdateCurrent(OnPress, interaction);
                    break;
            }
        }

        private void ReleasedOrExited(Interaction interaction)
        {
            switch (interaction)
            {
                case Interaction.Released:
                    ExecAndUpdateCurrent(OnRelease, Interaction.Entered);
                    break;
                case Interaction.Exited:
                    ExecAndUpdateCurrent(OnExit, Interaction.Untouched);
                    break;
            }
        }

        private void ExecAndUpdateCurrent(List<Action> actions, Interaction i)
        {
            Exec(actions);
            CurrentInteraction = i;
        }

        #endregion
    }
}