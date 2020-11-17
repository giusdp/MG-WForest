using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PiBa.UI.Widgets
{

    public struct Margin
    {
        public Margin(int left, int right, int top, int bottom)
        {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }

        public int Left, Right, Top, Bottom;
    }
    public abstract class Widget
    {
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
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        

        public virtual void StartedHovering() => OnEnter?.Invoke();
        public virtual void StoppedHovering() => OnExit?.Invoke();
        public virtual void PressedDown() => OnPress?.Invoke();
        public virtual void ReleasedPress() => OnRelease?.Invoke();
    }
}