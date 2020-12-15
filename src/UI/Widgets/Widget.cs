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
        #region Widget Data

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

        #endregion

        private readonly InteractionHandler _interactionHandler;

        protected Widget(Rectangle space)
        {
            Space = space;
            Margin = new Margin();
            Color = Color.White;
            Modifiers = new List<Action<SpriteBatch>>();
            _interactionHandler = new InteractionHandler();
        }

        public virtual void Update()
        {
            // Interaction handler:
            //     1. Check hovering
            //     2. Update
            _interactionHandler.Update();
        }

        public virtual void Draw(SpriteBatch spriteBatch) => Modifiers.ForEach(a => a(spriteBatch));

        #region Utils

        internal Interaction CurrentInteraction() => _interactionHandler.CurrentInteraction;
        internal void ChangeInteraction(Interaction interaction) => _interactionHandler.ChangeState(interaction);
        internal void AddOnEnter(Action onEnter) => _interactionHandler.OnEnter.Add(onEnter);
        internal void AddOnExit(Action onExit) => _interactionHandler.OnExit.Add(onExit);
        internal void AddOnPressed(Action onPress) => _interactionHandler.OnPress.Add(onPress);
        internal void AddOnRelease(Action onRelease) => _interactionHandler.OnRelease.Add(onRelease);

        #endregion
    }
}