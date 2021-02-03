using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WForest.Devices
{
    /// <summary>
    /// Interface to abstract over input devices such as mouse and controllers.
    /// </summary>
    public interface IDevice
    {
        /// <summary>
        /// Updates the state of the device.
        /// </summary>
        void Update();

        /// <summary>
        /// Get the (X,Y) coordinates on the screen pointed by the device.
        /// </summary>
        /// <returns>A Vector2 that holds the pointed coordinates on the game window.</returns>
        Vector2 GetPointedLocation();

        /// <summary>
        /// Returns true if the device interact button was just pressed.
        /// </summary>
        /// <returns></returns>
        bool IsPressed();

        /// <summary>
        /// Returns true if the device interact button was previously pressed and is held down.
        /// </summary>
        /// <returns></returns>
        bool IsHeld();

        /// <summary>
        /// Returns true if the device interact button was previously pressed and was just released.
        /// </summary>
        /// <returns></returns>
        bool IsReleased();
    }

    /// <summary>
    /// Device concrete class to handle Mouse location.
    /// </summary>
    public class MouseDevice : IDevice
    {
        private MouseDevice()
        {
            _leftMouseState = ButtonState.Released;
        }

        private static MouseDevice? _mouseDevice;

        private ButtonState _leftMouseState;
        private bool _wasLeftMPressed;

        /// <summary>
        /// Get the singleton instance of the MouseDevice class.
        /// </summary>
        public static MouseDevice Instance => _mouseDevice ??= new MouseDevice();

        /// <summary>
        /// Updates the mouse device getting the state of the left button each update.
        /// </summary>
        public void Update()
        {
            _wasLeftMPressed = _leftMouseState switch
            {
                ButtonState.Pressed when !_wasLeftMPressed => true,
                ButtonState.Released when _wasLeftMPressed => false,
                _ => _wasLeftMPressed
            };
            _leftMouseState = Mouse.GetState().LeftButton;
        }

        /// <summary>
        /// Get the (X,Y) coordinates of the mouse.
        /// </summary>
        /// <returns>A Vector2 that holds the cursor coordinates on the game window.</returns>
        public Vector2 GetPointedLocation()
        {
            var (x, y) = Mouse.GetState().Position;
            return new Vector2(x, y);
        }

        /// <summary>
        /// Returns true if the left mouse button was just clicked. 
        /// </summary>
        /// <returns></returns>
        public bool IsPressed()
            => _leftMouseState == ButtonState.Pressed && !_wasLeftMPressed;


        /// <summary>
        /// Returns true if the left mouse button is being held down.
        /// </summary>
        /// <returns></returns>
        public bool IsHeld()
            => _wasLeftMPressed && _leftMouseState == ButtonState.Pressed;


        /// <summary>
        /// Returns true if the left mouse button was just released.
        /// </summary>
        /// <returns></returns>
        public bool IsReleased() 
            => _leftMouseState == ButtonState.Released && _wasLeftMPressed;
    }
}