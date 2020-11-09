using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Serilog;

namespace PiBa.UI.Widgets
{
    public class Widget
    {
        public Rectangle Space { get; set; }

        public Widget(Rectangle space)
        {
            Space = space;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        public override string ToString() => $"Basic Widget";

        public virtual void StartedHovering() => Log.Debug($"Started Hovering on {ToString()}");

        public virtual void StoppedHovering() => Log.Debug($"Stopped Hovering on {ToString()}");

        public virtual void PressedDown() => Log.Debug($"Pressed on {ToString()}");
        public virtual void ReleasedPress() => Log.Debug($"Released press on {ToString()}");
    }
}