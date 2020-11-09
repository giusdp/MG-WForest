using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Serilog;

namespace PiBa.UI.Widgets
{
    public class ImageButton : Widget
    {
        private Texture2D _imageToDraw;
        public Texture2D NormalButton { get; set; }
        public Texture2D HoverButton { get; set; }
        public Texture2D PressedButton { get; set; }

        public ImageButton(Texture2D normalButton) : base(Rectangle.Empty)
        {
            NormalButton = normalButton ??
                           throw new ArgumentNullException($"{GetType()} Button sprite cannot be null!");
            _imageToDraw = NormalButton;
            Space = new Rectangle(0, 0, normalButton.Width, normalButton.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_imageToDraw, new Vector2(Space.X, Space.Y), Color.White);
        }

        public override void StartedHovering()
        {
            if (HoverButton == null) 
                Log.Warning($"{GetType()} HoverButton texture missing, fallback to NormalButton.");
            else _imageToDraw = HoverButton;
        }

        public override void StoppedHovering() => _imageToDraw = NormalButton;
        

        public override void PressedDown()
        {
            if (PressedButton == null) 
                Log.Warning($"{GetType()} PressedButton texture missing, fallback to NormalButton.");
            else _imageToDraw = PressedButton;
        }

        public override void ReleasedPress()
        {
            // Put here the onclick property?
            base.ReleasedPress();
        }

        public override string ToString() => $"ImageButton Widget";
    }
}