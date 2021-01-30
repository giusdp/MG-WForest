using System;
using Microsoft.Xna.Framework.Graphics;
using Serilog;
using WForest.UI.Props.Actions;
using WForest.Utilities;

namespace WForest.UI.Widgets.BuiltIn
{
    /// <summary>
    /// Widget that displays Texture2Ds based on hovering and pressed states which can be used as a button.
    /// </summary>
    public class ImageButton : Widget
    {
        private Texture2D _imageToDraw;
        private Texture2D NormalButton { get; }

        /// <summary>
        /// Texture2D to use when the widget is hovered. If not set it fallbacks to the normal button texture.
        /// </summary>
        public Texture2D? HoverButton { get; set; }

        /// <summary>
        /// Texture to use when the widget is pressed. If not set it fallbacks to the normal button texture.
        /// </summary>
        public Texture2D? PressedButton { get; set; }

        public ImageButton(Texture2D normalButton) : base(RectangleF.Empty)
        {
            NormalButton = normalButton ??
                           throw new ArgumentNullException(nameof(normalButton));
            _imageToDraw = NormalButton;
            Space = new RectangleF(0, 0, normalButton.Width, normalButton.Height);
            
            Props.AddProp(new OnEnter(StartedHovering));
            Props.AddProp(new OnEnter(StoppedHovering));
            Props.AddProp(new OnPress(PressedDown));
            Props.AddProp(new OnRelease(Released));
        }

        /// <summary>
        /// Draws the widget using based on the interaction state (if hovered or not, pressed or not)
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_imageToDraw, Space, Color);
        }

        #region Visualization Based On Interactions

        private bool _isHovering, _isPressed;

        private void StartedHovering()
        {
            if (_isHovering) return;

            _isHovering = true;

            if (HoverButton == null)
            {
                Log.Warning("ImageButton Widget: HoverButton texture missing, fallback to NormalButton");
                _imageToDraw = NormalButton;
            }
            else
                _imageToDraw = HoverButton;
        }

        private void StoppedHovering()
        {
            _isHovering = false;
            _isPressed = false;
            _imageToDraw = NormalButton;
        }

        private void PressedDown()
        {
            if (_isPressed == false)
            {
                _isPressed = true;
                _isHovering = false;
            }
            else return;

            if (PressedButton == null)
            {
                Log.Warning("ImageButton Widget: PressedButton texture missing, fallback to NormalButton");
                _imageToDraw = NormalButton;
            }
            else _imageToDraw = PressedButton;
        }

        private void Released() => _isPressed = false;

        #endregion
    }
}