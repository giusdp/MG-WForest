using System;
using System.Collections.Generic;

namespace WForest.UI.Widgets.Interactions
{
    public enum Interaction
    {
        Untouched,
        Entered,
        Exited,
        Pressed,
        Released
    }

    public class InteractStateMachine
    {
        public List<Action> OnEnter { get; }
        public List<Action> OnExit { get; }
        public List<Action> OnPress { get; }
        public List<Action> OnRelease { get; }

        private Interaction _currentState;

        public InteractStateMachine()
        {
            OnEnter = new List<Action>();
            OnExit = new List<Action>();
            OnPress = new List<Action>();
            OnRelease = new List<Action>();
            _currentState = Interaction.Untouched;
        }

        public void ChangeState(Interaction interaction) => _currentState = interaction;

        public void Update()
        {
            switch (_currentState)
            {
                case Interaction.Untouched:
                    break;
                case Interaction.Entered:
                    OnEnter.ForEach(a => a());
                    break;
                case Interaction.Exited:
                    OnExit.ForEach(a => a());
                    break;
                case Interaction.Pressed:
                    OnPress.ForEach(a => a());
                    break;
                case Interaction.Released:
                    OnRelease.ForEach(a => a());
                    break;
            }
        }
    }
}