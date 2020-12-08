using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Serilog;

namespace WForest.UI.Widgets
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
                           throw new ArgumentNullException(nameof(normalButton));
            _imageToDraw = NormalButton;
            Space = new Rectangle(0, 0, normalButton.Width, normalButton.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_imageToDraw, Space, Color.White);
            base.Draw(spriteBatch);
        }

        public override void StartedHovering()
        {
            if (HoverButton == null)
            {
                Log.Warning($"{GetType()} HoverButton texture missing, fallback to NormalButton.");
                _imageToDraw = NormalButton;
            }
            else _imageToDraw = HoverButton;

            base.StartedHovering();
        }

        public override void StoppedHovering()
        {
            _imageToDraw = NormalButton;
            base.StoppedHovering();
        }

        public override void PressedDown()
        {
            if (PressedButton == null)
            {
                Log.Warning($"{GetType()} PressedButton texture missing, fallback to NormalButton.");
                _imageToDraw = NormalButton;
            }
            else _imageToDraw = PressedButton;

            base.PressedDown();
        }

    }
}