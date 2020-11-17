using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PiBa.UI.Widgets
{
    public abstract class Widget
    {
        public Rectangle Space { get; set; }
        public Rectangle Margin { get; set; }

        public Rectangle TotalSpaceOccupied =>
            new Rectangle(
                Space.X - Margin.X,
                Space.Y - Margin.Y,
                Space.Width + Margin.Width,
                Space.Height + Margin.Height
            );

        public Action OnEnter { private get; set; }
        public Action OnExit { private get; set; }
        public Action OnPress { private get; set; }
        public Action OnRelease { private get; set; }

        protected Widget(Rectangle space)
        {
            Space = space;
            Margin = Rectangle.Empty;
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        

        public virtual void StartedHovering() => OnEnter?.Invoke();
        public virtual void StoppedHovering() => OnExit?.Invoke();
        public virtual void PressedDown() => OnPress?.Invoke();
        public virtual void ReleasedPress() => OnRelease?.Invoke();
    }
}