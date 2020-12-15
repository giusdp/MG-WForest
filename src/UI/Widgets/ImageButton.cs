using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Serilog;

namespace WForest.UI.Widgets
{
    public class ImageButton : Widget
    {
        private Texture2D _imageToDraw;
        private Texture2D NormalButton { get; }
        public Texture2D HoverButton { get; set; }
        public Texture2D PressedButton { get; set; }

        public ImageButton(Texture2D normalButton) : base(Rectangle.Empty)
        {
            NormalButton = normalButton ??
                           throw new ArgumentNullException(nameof(normalButton));
            _imageToDraw = NormalButton;
            Space = new Rectangle(0, 0, normalButton.Width, normalButton.Height);

            AddOnEnter(StartedHovering);
            AddOnExit(StoppedHovering);
            AddOnPressed(PressedDown);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_imageToDraw, Space, Color);
            base.Draw(spriteBatch);
        }

        #region Visualization Based On Interactions

        private void StartedHovering()
        {
            if (HoverButton == null)
            {
                Log.Warning($"{GetType()} HoverButton texture missing, fallback to NormalButton.");
                _imageToDraw = NormalButton;
            }
            else _imageToDraw = HoverButton;
        }

        private void StoppedHovering()
        {
            _imageToDraw = NormalButton;
        }

        private void PressedDown()
        {
            if (PressedButton == null)
            {
                Log.Warning($"{GetType()} PressedButton texture missing, fallback to NormalButton.");
                _imageToDraw = NormalButton;
            }
            else _imageToDraw = PressedButton;
        }

        #endregion
    }
}