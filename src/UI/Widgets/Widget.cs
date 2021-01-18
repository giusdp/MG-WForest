using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WForest.UI.Widgets.Interactions;
using WForest.Utilities;

namespace WForest.UI.Widgets
{
    /// <summary>
    /// Base class for Widgets. It contains the main data used to handle and draw widgets.
    /// </summary>
    public abstract class Widget
    {
        #region Widget Data

        /// <summary>
        /// The Widget color.
        /// </summary>
        public Color Color;

        /// <summary>
        /// The Space used by the widget.
        /// </summary>
        public Rectangle Space { get; set; }
        
        /// <summary>
        /// The values for the left, right, top, bottom margins.
        /// </summary>
        public MarginValues MarginValues { get; set; }

        internal Rectangle TotalSpaceOccupied =>
            new Rectangle(
                Space.X - MarginValues.Left,
                Space.Y - MarginValues.Top,
                Space.Width + MarginValues.Left + MarginValues.Right,
                Space.Height + MarginValues.Top + MarginValues.Bottom
            );

        internal List<Action<SpriteBatch>> PostDrawing { get; }

        #endregion

        private readonly InteractionHandler _interactionHandler;

        /// <summary>
        /// Base constructor that takes the destination space for the widget.
        /// </summary>
        /// <param name="space"></param>
        protected Widget(Rectangle space)
        {
            Space = space;
            MarginValues = new MarginValues();
            Color = Color.White;
            PostDrawing = new List<Action<SpriteBatch>>();
            _interactionHandler = new InteractionHandler();
        }

        /// <summary>
        /// Updates the widget and checks for interactions. Override it to add custom logic, base.Update()
        /// is needed to keep the interactions checks.
        /// </summary>
        public virtual void Update()
        {
            _interactionHandler.Update();
        }

        internal void DrawAndPostDraw(SpriteBatch spriteBatch)
        {
            Draw(spriteBatch);
            PostDrawing.ForEach(a => a(spriteBatch));
        }

        /// <summary>
        /// Draw the widget with a SpriteBatch.
        /// </summary>
        /// <param name="spriteBatch"></param>
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