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
        public List<Action<SpriteBatch>> PostDrawing { get; }
        public Rectangle Space { get; set; }
        public MarginValues MarginValues { get; set; }

        public Rectangle TotalSpaceOccupied =>
            new Rectangle(
                Space.X - MarginValues.Left,
                Space.Y - MarginValues.Top,
                Space.Width + MarginValues.Left + MarginValues.Right,
                Space.Height + MarginValues.Top + MarginValues.Bottom
            );

        #endregion

        private readonly InteractionHandler _interactionHandler;

        protected Widget(Rectangle space)
        {
            Space = space;
            MarginValues = new MarginValues();
            Color = Color.White;
            PostDrawing = new List<Action<SpriteBatch>>();
            _interactionHandler = new InteractionHandler();
        }

        public virtual void Update()
        {
            _interactionHandler.Update();
        }

        internal void DrawAndPostDraw(SpriteBatch spriteBatch)
        {
            Draw(spriteBatch);
            PostDrawing.ForEach(a => a(spriteBatch));
        }

        public virtual void Draw(SpriteBatch spriteBatch) {}

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