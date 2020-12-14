using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WForest.UI.Widgets.Interactions;
using WForest.Utilities;

namespace WForest.UI.Widgets
{
    public abstract class Widget
    {
        public Color Color;
        public List<Action<SpriteBatch>> Modifiers { get; }
        public Rectangle Space { get; set; }
        public Margin Margin { get; set; }

        public Rectangle TotalSpaceOccupied =>
            new Rectangle(
                Space.X - Margin.Left,
                Space.Y - Margin.Top,
                Space.Width + Margin.Left + Margin.Right,
                Space.Height + Margin.Top + Margin.Bottom
            );

        private readonly InteractStateMachine _interactStateMachine;
      
        protected Widget(Rectangle space)
        {
            Space = space;
            Margin = new Margin();
            Color = Color.White;
            Modifiers = new List<Action<SpriteBatch>>();
            _interactStateMachine = new InteractStateMachine();
        }

        public virtual void Update()
        {
            // Interaction handler:
            //     1. Check hovering
            //     2. Update
            _interactStateMachine.Update();
        }

        public virtual void Draw(SpriteBatch spriteBatch) => Modifiers.ForEach(a => a(spriteBatch));

        #region Utils

        internal void ChangeInteraction(Interaction interaction) => _interactStateMachine.ChangeState(interaction);
        internal void AddOnEnter(Action onEnter) => _interactStateMachine.OnEnter.Add(onEnter);
        internal void AddOnExit(Action onExit) => _interactStateMachine.OnExit.Add(onExit);
        internal void AddOnPressed(Action onPress) => _interactStateMachine.OnPress.Add(onPress);
        internal void AddOnRelease(Action onRelease) => _interactStateMachine.OnRelease.Add(onRelease);

        #endregion
    }
}