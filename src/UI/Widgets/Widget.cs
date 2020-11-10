using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PiBa.UI.Widgets
{
    public class Widget
    {
        public Rectangle Space { get; set; }
        
        public Action OnEnter { private get; set; }
        public Action OnExit { private get; set; }
        public Action OnPress { private get; set; }
        public Action OnRelease { private get; set; }

        public Widget(Rectangle space)
        {
            Space = space;
        }

        public virtual void Draw(SpriteBatch spriteBatch){}

        public virtual void StartedHovering() => OnEnter?.Invoke();
        public virtual void StoppedHovering() => OnExit?.Invoke();
        public virtual void PressedDown() => OnPress?.Invoke();
        public virtual void ReleasedPress() => OnRelease?.Invoke();
    }
}