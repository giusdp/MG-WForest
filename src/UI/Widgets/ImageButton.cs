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

        private Effect _effect = AssetLoader.Load<Effect>("Shaders/Rounded");
        public override void Draw(SpriteBatch spriteBatch)
        {
            _effect.Parameters["Width"].SetValue(Space.Width);
            _effect.Parameters["Height"].SetValue(Space.Height);
            _effect.Parameters["Radius"].SetValue(30);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, effect: _effect);
            spriteBatch.Draw(_imageToDraw, new Vector2(Space.X, Space.Y), Color.White);
            
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