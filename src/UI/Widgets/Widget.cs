using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        public override string ToString() => $"Basic Widget with a Space of {Space}";

        public void StartedHovering()
        {
            Console.WriteLine("Started Hovering on: " + ToString());
        }

        public void StoppedHovering()
        {
            Console.WriteLine("Stopped Hovering on: " + ToString());
        }
    }
}