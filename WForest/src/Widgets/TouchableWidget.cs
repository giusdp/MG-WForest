using Serilog;
using WForest.Props.Actions;
using WForest.Rendering;
using WForest.Rendering.DrawableAdapters;
using WForest.Utilities;

namespace WForest.Widgets
{
    /// <summary>
    /// A TouchableWidget provides a neutral state, hover state and pressed state textures and handles the texture to
    /// draw swap based on the interaction. TODO find a better name cause all widgets are already touchable
    /// </summary>
    public abstract class TouchableWidget : Widget
    {
        private Drawable _objToDraw;
        private bool _isHovering, _isPressed;
        public Drawable NormalTexture { get; set; }
        public Drawable? HoverTexture { get; set; }
        public Drawable? PressTexture { get; set; }


        protected TouchableWidget(RectangleF space, Drawable normal, Drawable? hover = null, Drawable? press = null)
            : base(space)
        {
            NormalTexture = normal;
            _objToDraw = NormalTexture;
            HoverTexture = hover;
            PressTexture = press;

            Props.AddProp(new OnEnter(StartedHovering));
            Props.AddProp(new OnExit(StoppedHovering));
            Props.AddProp(new OnPress(PressedDown));
            Props.AddProp(new OnRelease(Released));
        }

        protected TouchableWidget(Drawable normal, Drawable? hover = null, Drawable? press = null)
            : this(new RectangleF(0, 0, normal.Width, normal.Height), normal, hover, press)
        {
        }

        /// <summary>
        /// Draws the widget using based on the interaction state (if hovered or not, pressed or not)
        /// </summary>
        /// <param name="renderer"></param>
        public override void Draw(IRenderer renderer)
        {
            _objToDraw.Draw(renderer, Space, Color);
            base.Draw(renderer);
        }

        #region Visualization Based On Interactions

        private void StartedHovering()
        {
            if (_isHovering) return;

            _isHovering = true;

            if (HoverTexture == null)
            {
                Log.Warning("TouchableWidget: HoverButton drawable missing, fallback to NormalButton");
                _objToDraw = NormalTexture!;
            }
            else
                _objToDraw = HoverTexture;
        }

        private void StoppedHovering()
        {
            _isHovering = false;
            _isPressed = false;
            _objToDraw = NormalTexture!;
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
                Log.Warning("TouchableWidget: PressedButton drawable missing, fallback to NormalButton");
                _objToDraw = NormalTexture!;
            }
            else _objToDraw = PressTexture;
        }

        private void Released()
        {
            _isPressed = false;
            StartedHovering();
        }

        #endregion
    }
}