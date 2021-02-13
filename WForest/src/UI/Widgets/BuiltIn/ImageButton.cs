using System;
using Microsoft.Xna.Framework.Graphics;
using Serilog;
using WForest.Rendering;
using WForest.UI.Props.Actions;
using WForest.Utilities;

namespace WForest.UI.Widgets.BuiltIn
{
    /// <summary>
    /// Widget that displays Texture2Ds based on hovering and pressed states which can be used as a button.
    /// </summary>
    public class ImageButton : TexturedWidget
    {
        private Texture2D _imageToDraw;

        public ImageButton(Texture2D normalButton, Texture2D? hoverButton = null, Texture2D? pressButton = null) : 
            base(new RectangleF(0, 0, normalButton.Width, normalButton.Height))
        {
            NormalTexture = normalButton ??
                            throw new ArgumentNullException(nameof(normalButton));
            HoverTexture = hoverButton;
            PressTexture = pressButton;

            _imageToDraw = NormalTexture;

            Props.AddProp(new OnEnter(StartedHovering));
            Props.AddProp(new OnExit(StoppedHovering));
            Props.AddProp(new OnPress(PressedDown));
            Props.AddProp(new OnRelease(Released));
        }

        /// <summary>
        /// Draws the widget using based on the interaction state (if hovered or not, pressed or not)
        /// </summary>
        /// <param name="renderer"></param>
        public override void Draw(IRenderer renderer)
        {
            renderer.Draw(_imageToDraw, Space, Color);
            base.Draw(renderer);
        }

        #region Visualization Based On Interactions

        private bool _isHovering, _isPressed;

        private void StartedHovering()
        {
            if (_isHovering) return;

            _isHovering = true;

            if (HoverTexture == null)
            {
                Log.Warning("ImageButton Widget: HoverButton texture missing, fallback to NormalButton");
                _imageToDraw = NormalTexture;
            }
            else
                _imageToDraw = HoverTexture;
        }

        private void StoppedHovering()
        {
            _isHovering = false;
            _isPressed = false;
            _imageToDraw = NormalTexture;
        }

        private void PressedDown()
        {
            if (_isPressed == false)
            {
                _isPressed = true;
                _isHovering = false;
            }
            else return;

            if (PressTexture == null)
            {
                Log.Warning("ImageButton Widget: PressedButton texture missing, fallback to NormalButton");
                _imageToDraw = NormalTexture;
            }
            else _imageToDraw = PressTexture;
        }

        private void Released()
        {
            _isPressed = false;
            StartedHovering();
        }

        #endregion
    }
}