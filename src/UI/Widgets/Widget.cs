using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PiBa.Utilities;

namespace PiBa.UI.Widgets
{
    public abstract class Widget
    {
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

        public Action OnEnter { private get; set; }
        public Action OnExit { private get; set; }
        public Action OnPress { private get; set; }
        public Action OnRelease { private get; set; }

        protected Widget(Rectangle space)
        {
            Space = space;
            Margin = new Margin();
            Modifiers = new List<Action<SpriteBatch>>();
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        public virtual void StartedHovering() => OnEnter?.Invoke();
        public virtual void StoppedHovering() => OnExit?.Invoke();
        public virtual void PressedDown() => OnPress?.Invoke();
        public virtual void ReleasedPress() => OnRelease?.Invoke();
    }
}